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

        private StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <param name="errMsg"></param>
        public void DeleteAll(out string errMsg)
        {
            sb.Length = 0;
            sb.Append("DELETE FROM tResult");
            DataBase.ExecuteCommand(sb.ToString(),out errMsg);
            sb.Length = 0;
        }

		/// <summary>
		/// 部分修改保存
		/// </summary>
		/// <param name="model">对象clsResult的一个实例参数</param>
		/// <returns></returns>
		public int UpdatePart(clsResult model,out string errMsg)
		{
			errMsg=string.Empty; 
			int rtn=0;
            sb.Length=0;
            try
            {
                sb.Append("UPDATE tResult SET ");

                sb.AppendFormat("CheckNo='{0}',", model.CheckNo);
                sb.AppendFormat("StdCode='{0}',", model.StdCode);
                sb.AppendFormat("SampleCode='{0}',", model.SampleCode);
                sb.AppendFormat("CheckedCompany='{0}',", model.CheckedCompany);
                sb.AppendFormat("CheckedCompanyName='{0}',", model.CheckedCompanyName);
                sb.AppendFormat("CheckedComDis='{0}',", model.CheckedComDis);
                sb.AppendFormat("CheckPlaceCode='{0}',", model.CheckPlaceCode);
                sb.AppendFormat("FoodCode='{0}',", model.FoodCode);
                if (model.ProduceDate != null)
                {
                    sb.AppendFormat("ProduceDate='{0}',", model.ProduceDate);
                }
                if (model.ProduceDate == null)
                {
                    sb.AppendFormat("ProduceDate=null,", "");
                }
                sb.AppendFormat("ProduceCompany='{0}',", model.ProduceCompany);
                sb.AppendFormat("ProducePlace='{0}',", model.ProducePlace);
                sb.AppendFormat("SentCompany='{0}',", model.SentCompany);
                sb.AppendFormat("Provider='{0}',", model.Provider);
                sb.AppendFormat("TakeDate='{0}',", model.TakeDate);
                sb.AppendFormat("CheckStartDate='{0}',", model.CheckStartDate);
                sb.AppendFormat("CheckEndDate='{0}',", model.CheckEndDate);
                sb.AppendFormat("ImportNum='{0}',", model.ImportNum);
                sb.AppendFormat("SaveNum='{0}',", model.SaveNum);
                sb.AppendFormat("Unit='{0}',", model.Unit);
                if (model.SampleBaseNum != "null")
                {
                    sb.AppendFormat("SampleBaseNum={0},", model.SampleBaseNum);
                }
                if (model.SaleNum != "null")
                {
                    sb.AppendFormat("SampleNum={0},", model.SampleNum);
                }
                sb.AppendFormat("SampleUnit='{0}',", model.SampleUnit);
                sb.AppendFormat("SampleLevel='{0}',", model.SampleLevel);
                sb.AppendFormat("SampleModel='{0}',", model.SampleModel);
                sb.AppendFormat("SampleState='{0}',", model.SampleState);
                sb.AppendFormat("Standard='{0}',", model.Standard);
                sb.AppendFormat("CheckMachine='{0}',", model.CheckMachine);
                sb.AppendFormat("CheckTotalItem='{0}',", model.CheckTotalItem);
                sb.AppendFormat("CheckValueInfo='{0}',", model.CheckValueInfo);
                sb.AppendFormat("StandValue='{0}',", model.StandValue);
                sb.AppendFormat("Result='{0}',", model.Result);
                sb.AppendFormat("ResultInfo='{0}',", model.ResultInfo);
                sb.AppendFormat("UpperCompany='{0}',", model.UpperCompany);
                sb.AppendFormat("UpperCompanyName='{0}',", model.UpperCompanyName);
                sb.AppendFormat("OrCheckNo='{0}',", model.OrCheckNo);
                sb.AppendFormat("CheckType='{0}',", model.CheckType);
                sb.AppendFormat("CheckUnitCode='{0}',", model.CheckUnitCode);
                sb.AppendFormat("Checker='{0}',", model.Checker);
                sb.AppendFormat("Assessor='{0}',", model.Assessor);
                sb.AppendFormat("Organizer='{0}',", model.Organizer);
                sb.AppendFormat("Remark='{0}',", model.Remark);
                sb.AppendFormat("CheckPlanCode='{0}',", model.CheckPlanCode);
                if (model.SaleNum != "null")
                {
                    sb.AppendFormat("SaleNum={0},", model.SaleNum);
                }
                if (model.Price != "null")
                {
                    sb.AppendFormat("Price={0},", model.Price);
                }
                sb.AppendFormat("CheckederVal='{0}',", model.CheckederVal);
                sb.AppendFormat("IsSentCheck='{0}',", model.IsSentCheck);
                sb.AppendFormat("CheckederRemark='{0}',", model.CheckederRemark);
                //sb.AppendFormat("ParentCompanyName='{0}',",model .ParentCompanyName );
                sb.AppendFormat("Notes='{0}'", model.Notes);
                sb.AppendFormat(" WHERE SysCode='{0}'", model.SysCode);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);

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
            sb.Length=0;
            sb.Append("SELECT COUNT(1) FROM tResult ");
            if (strWhere.Length > 0)
            {
                sb.Append(" WHERE ");
                sb.Append(strWhere);
            }
            object obj = DataBase.GetOneValue(sb.ToString(), out errMsg);
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
            sb.Length=0;
            try
            {
                sb.AppendFormat("DELETE FROM tResult WHERE SysCode='{0}'", mainkey);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);

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
            sb.Length=0;
            try
            {
                sb.Append("SELECT ");//新两个字段
                sb.Append("A.IsSended,A.SysCode, A.CheckNo,A.SampleCode,A.CheckPlanCode, A.CheckedCompany, A.CheckedCompanyName,");
                sb.Append("A.CheckedComDis, A.UpperCompany,A.UpperCompanyName, A.FoodCode, A.FoodName,");
                sb.Append("A.CheckType, A.SampleModel, A.SampleLevel, A.SampleState, A.Provider, ");
                sb.Append("A.StdCode, A.OrCheckNo, A.ProduceCompany, A.ProduceCompanyName,A.ProducePlace,A.ProducePlaceName,");
                sb.Append("A.ProduceDate, A.ImportNum,A.SaleNum, A.SaveNum, A.Price,A.Unit, A.SampleNum, A.SampleBaseNum,");
                sb.Append("A.SampleUnit, A.SentCompany, A.Remark,A.CheckederVal,A.CheckederRemark,A.Notes, ");
                sb.Append("format(A.TakeDate,\"yyyy-mm-dd\")AS TakeDate,");

                sb.Append("A.OrganizerName, A.CheckTotalItem, A.CheckTotalItemName, A.Standard, A.StandardName,");
                sb.Append("A.CheckValueInfo,A.ResultInfo,A.StandValue, A.Result,A.IsSentCheck,");
                //sb.Append("A.CheckStartDate,");
                 sb.Append("Format$(A.CheckStartDate,\"General Date\")AS CheckStartDate,");
                //sb.Append("Format$(A.CheckStartDate,\"yyyy-mm-dd hh:mm:ss\")AS CheckStartDate,");
                sb.Append("A.Checker, A.CheckerName, A.Assessor, A.AssessorName,");
                sb.Append("A.CheckUnitCode,A.CheckUnitName,A.CheckUnitInfo,"); //新增加
                sb.Append("A.ResultType,A.HolesNum,A.MachineSampleNum, ");
                sb.Append("A.MachineName,A.MachineModel,A.MachineCompany, A.SendedDate,");
                sb.Append("A.Sender,P.Name AS SenderName,A.IsReSended, A.CheckPlaceCode, A.CheckPlace,");
                sb.Append("A.CheckEndDate, A.CheckMachine,A.Organizer ");

                sb.Append(" FROM [SELECT T11.*,O.ItemDes As CheckTotalItemName");
                sb.Append(" FROM (SELECT T10.*,S.StdDes As StandardName");
                sb.Append(" FROM (SELECT T8.*,Q.Name As ProducePlaceName");
                sb.Append(" FROM (SELECT T6.*,N.Name As OrganizerName");
                sb.Append(" FROM (SELECT T5.*,M.Name As AssessorName");
                sb.Append(" FROM (SELECT T4.*,L.FullName As ProduceCompanyName");
                sb.Append(" FROM (SELECT T3.*,K.MachineName,K.MachineModel,K.Company AS MachineCompany");//新增加
                sb.Append(" FROM (SELECT T2.*,J.Name AS CheckPlace");
                sb.Append(" FROM (SELECT C.*,B.Name AS FoodName,I.Name AS CheckerName,");
                sb.Append(" H.FullName AS CheckUnitName,H.ShortName AS CheckUnitInfo"); //新增加
                sb.Append(" FROM tResult AS C,tFoodClass AS B,tUserUnit AS H, tUserInfo AS I");
                sb.Append(" WHERE C.FoodCode=B.SysCode AND C.CheckUnitCode=H.SysCode AND C.Checker=I.UserCode)AS T2");
                sb.Append(" LEFT JOIN tDistrict AS J On T2.CheckPlaceCode =J.SysCode)AS T3");
                sb.Append(" LEFT JOIN tMachine AS K On T3.CheckMachine=K.SysCode)AS T4");
                sb.Append(" LEFT JOIN tCompany AS L On T4.ProduceCompany=L.SysCode)AS T5");
                sb.Append(" LEFT JOIN tUserInfo AS M On T5.Assessor=M.UserCode)As T6");
                sb.Append(" LEFT JOIN tUserInfo AS N On T6.Organizer=N.UserCode)As T8");
                sb.Append(" LEFT JOIN tProduceArea AS Q On T8.ProducePlace=Q.SysCode)As T10");
                sb.Append(" LEFT JOIN tStandard AS S On T10.Standard=S.SysCode)AS T11");
                sb.Append(" LEFT JOIN tCheckItem AS O On T11.CheckTotalItem=O.SysCode]. AS A");
                sb.Append(" LEFT JOIN tUserInfo AS P ON A.Sender=P.UserCode ");

                if (!whereSql.Equals(""))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                }
                if (!orderBySql.Equals(""))
                {
                    sb.Append(" ORDER BY ");
                    sb.Append(orderBySql);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "Result" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sb.Length = 0;
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
        public DataTable GetUploadDataTable(string strWhere, string orderBy,string appTag)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;
            sb.Length=0;
            try
            {
                sb.Append("SELECT ");//新两个字段
                sb.Append("A.SysCode,A.ResultType, A.CheckNo,A.SampleCode,A.StdCode,");
                sb.Append("A.CheckedCompany AS CheckedCompanyCode,");//企业编号

                //如是是工商版/农检片本是两级 要查询"UpperCompanyName--CheckedCompany"
                //如果是食药 餐饮版要查询"CheckedCompanyName--CheckedCompany"

                    sb.Append("A.UpperCompanyName AS CheckedCompany, ");


                sb.Append("A.CheckedCompanyName AS CheckedCompanyInfo,A.CheckedComDis,");
                sb.Append("A.CheckPlaceCode,");
                sb.Append("(SELECT J.Name FROM tDistrict AS J WHERE A.CheckPlaceCode =J.SysCode)AS CheckPlace,");//检测机构名称
                //if(  )
                //{
                sb.Append("(select z.Cdcode from tProprietors as z where z.Cdname=A.CheckedCompanyName and z.Ciname=A.UpperCompanyName)as CheckedDealerCode,");//被检经营户编号
                //}
                sb.Append("(SELECT B.Name FROM tFoodClass AS B WHERE A.FoodCode=B.SysCode)AS FoodName,");//查询食品名称 A.FoodCode,
                sb.Append("A.SentCompany,A.Provider,A.ProduceDate,");
                sb.Append("(SELECT L.FullName FROM tCompany AS L WHERE A.ProduceCompany=L.SysCode)AS ProduceCompany,");//A.ProduceCompanyName 

                sb.Append("(SELECT Q.Name FROM tProduceArea AS Q WHERE Q.SysCode=A.ProducePlace)AS ProducePlace,");//查询产地名称A.ProducePlace,

                //区域目前只构造五级
                sb.Append("( IIf(Len(A.ProducePlace)=6,(SELECT R.Name FROM tProduceArea AS R WHERE R.SysCode=MID(A.ProducePlace,1,6)),'')+ IIf(Len(A.ProducePlace)>6,(SELECT R.Name FROM tProduceArea AS R WHERE R.SysCode=MID(A.ProducePlace,1,9))+'/','') + IIf(Len(A.ProducePlace)>9,(SELECT R.Name FROM tProduceArea AS R WHERE R.SysCode=MID(A.ProducePlace,1,12))+'/','') + IIf(Len(A.ProducePlace)>12,(SELECT R.Name FROM tProduceArea AS R WHERE R.SysCode=MID(A.ProducePlace,1,15))+'/','')+ IIf(Len(A.ProducePlace)>15,(SELECT R.Name FROM tProduceArea AS R WHERE R.SysCode=MID(A.ProducePlace,1,18))+'/','')+IIf(Len(A.ProducePlace)>18,(SELECT R.Name FROM tProduceArea AS R WHERE R.SysCode=MID(A.ProducePlace,1,21))+'/','') ) AS ProducePlaceInfo,"); //ProducePlaceInfo 需要这个字段

                sb.Append("A.ImportNum,A.SaleNum, A.SaveNum, A.Price,A.SampleNum, A.SampleBaseNum,");
                sb.Append("A.TakeDate,");
                sb.Append("A.CheckStartDate,A.CheckEndDate,");
                sb.Append("A.Unit,A.SampleUnit,A.SampleModel, A.SampleLevel,A.SampleState,");

                sb.Append("(SELECT S.StdDes FROM tStandard AS S WHERE A.Standard=S.SysCode)AS Standard,");
                sb.Append("K.MachineName AS CheckMachine,K.MachineModel AS CheckMachineModel,K.Company AS MachineCompany,");
                sb.Append("(SELECT O.ItemDes FROM tCheckItem AS O WHERE O.SysCode=A.CheckTotalItem)AS CheckTotalItem,");//查询检测项目名称 

                sb.Append("A.CheckValueInfo,A.StandValue, A.Result,A.ResultInfo,A.OrCheckNo,");
                sb.Append("A.UpperCompany,A.UpperCompanyName,");
                sb.Append("A.CheckType,");
                sb.Append("H.FullName AS CheckUnitName,H.ShortName AS CheckUnitInfo,"); //联合新增加A.CheckUnitCode,
      
                sb.Append("(SELECT I.Name FROM tUserInfo AS I WHERE I.UserCode=A.Checker) AS Checker,");//A.Organizer
                sb.Append("(SELECT M.Name FROM tUserInfo AS M WHERE M.UserCode=A.Assessor) AS Assessor,");//A.Organizer Assessor
                sb.Append("(SELECT N.Name FROM tUserInfo AS N WHERE N.UserCode=A.Organizer) AS Organizer,");//A.Organizer
                sb.Append("A.Remark,A.CheckPlanCode,A.CheckederVal,A.IsSentCheck,A.CheckederRemark,");
                sb.Append("IIf(A.IsReSended, '是', '否')AS IsReSended,");
                sb.Append("'1' AS UpLoader,A.Notes");
                sb.Append(" FROM (tResult AS A LEFT JOIN tUserUnit AS H ON A.CheckUnitCode=H.SysCode)");//联合检测单位
                sb.Append(" LEFT JOIN tMachine AS K On A.CheckMachine=K.SysCode");
  
                if (!strWhere.Equals(""))
                {
                    sb.Append(" WHERE ");
                    sb.Append(strWhere);
                }
                if (!orderBy.Equals(""))
                {
                    sb.Append(" ORDER BY ");
                    sb.Append(orderBy);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "Result" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sb.Length = 0;
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
            sb.Length=0;
            try
            {
                sb.Append("SELECT A.IsSended, A.SendedDate, A.Sender, P.Name AS SenderName,");
                sb.Append("A.IsReSended,A.SysCode, A.CheckNo,A.CheckPlanCode, A.CheckedCompany,");
                sb.Append("A.CheckedCompanyName,A.CheckedComDis, A.UpperCompany,A.UpperCompanyName,A.FoodCode, A.FoodName,"); 
                sb.Append("A.CheckType, A.SampleModel, A.SampleLevel, A.SampleState, A.Provider, A.StdCode, A.OrCheckNo,");
                sb.Append("A.ProduceCompany, A.ProduceCompanyName,A.ProducePlace,A.ProducePlaceName, A.ProduceDate,");
                sb.Append("A.ImportNum,A.SaleNum, A.SaveNum, A.Price,A.Unit, A.SampleNum, A.SampleBaseNum, A.SampleUnit,");
                sb.Append("A.SentCompany, A.Remark,A.CheckederVal,A.CheckederRemark,A.Notes, A.TakeDate, A.OrganizerName,");
                sb.Append("A.CheckTotalItem, A.CheckTotalItemName, A.Standard, A.StandardName, "); 
                sb.Append("A.CheckValueInfo,A.ResultInfo,A.StandValue,A.SampleCode, A.Result,");
                sb.Append("A.IsSentCheck, A.CheckStartDate, A.Checker, A.CheckerName, A.Assessor, A.AssessorName,");
                sb.Append("A.CheckUnitCode, A.CheckUnitName,A.ResultType, A.MachineName, A.CheckPlaceCode, A.CheckPlace,");
                sb.Append("A.CheckEndDate, A.CheckMachine,  A.Organizer ");

                sb.Append(" FROM [SELECT T11.*,O.ItemDes As CheckTotalItemName ");
                sb.Append(" FROM (SELECT T10.*,S.StdDes As StandardName");
                sb.Append(" FROM (SELECT T8.*,Q.Name As ProducePlaceName");
                sb.Append(" FROM (SELECT T6.*,N.Name As OrganizerName");
                sb.Append(" FROM (SELECT T5.*,M.Name As AssessorName");
                sb.Append(" FROM (SELECT T4.*,L.FullName As ProduceCompanyName");
                sb.Append(" FROM (SELECT T3.*,K.MachineName");
                sb.Append(" FROM (SELECT T2.*,J.Name AS CheckPlace");
                sb.Append(" FROM (SELECT C.*, B.Name AS FoodName,H.FullName AS CheckUnitName,I.Name AS CheckerName");
                sb.Append(" FROM tResult AS C, tFoodClass AS B, tUserUnit AS H, tUserInfo AS I");
                sb.Append(" WHERE C.FoodCode=B.SysCode And  C.CheckUnitCode=H.SysCode And C.Checker=I.UserCode) As T2");
                sb.Append(" LEFT JOIN tDistrict As J On T2.CheckPlaceCode =J.SysCode) As T3");
                sb.Append(" LEFT JOIN tMachine As K On T3.CheckMachine=K.SysCode) As T4");
                sb.Append(" LEFT JOIN tCompany As L On T4.ProduceCompany=L.SysCode) As T5");
                sb.Append(" LEFT JOIN tUserInfo As M On T5.Assessor=M.UserCode) As T6");
                sb.Append(" LEFT JOIN tUserInfo As N On T6.Organizer=N.UserCode) As T8");
                sb.Append(" LEFT JOIN tProduceArea As Q On T8.ProducePlace=Q.SysCode) As T10");
                sb.Append(" LEFT JOIN tStandard As S On T10.Standard=S.SysCode) As T11");
                sb.Append(" LEFT JOIN tCheckItem As O On T11.CheckTotalItem=O.SysCode]. AS A");
                sb.Append(" LEFT JOIN tUserInfo AS P ON A.Sender=P.UserCode");

                if (!whereSql.Equals(""))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                }
                if (!orderBySql.Equals(""))
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
            sb.Length=0;
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Append("INSERT INTO tResult");
                sb.Append("(SysCode,ResultType,CheckNo,StdCode,SampleCode,CheckedCompany,");
                sb.Append("CheckedCompanyName,CheckedComDis,CheckPlaceCode,FoodCode,");
                if (model.ProduceDate != null)
                {
                    sb.Append("ProduceDate,");
                }

                sb.Append("ProduceCompany,ProducePlace,SentCompany,Provider,TakeDate,CheckStartDate,CheckEndDate,");
                sb.Append("ImportNum,SaveNum,Unit,SampleBaseNum,SampleNum,SampleUnit,SampleLevel,SampleModel,SampleState,");
                sb.Append("Standard,CheckMachine,CheckTotalItem,CheckValueInfo,StandValue,Result,ResultInfo,UpperCompany,UpperCompanyName,");
                sb.Append("OrCheckNo,CheckType,CheckUnitCode,Checker,Assessor,Organizer,Remark,CheckPlanCode,SaleNum,Price,CheckederVal,");
                sb.Append("IsSentCheck,CheckederRemark,Notes, HolesNum,MachineSampleNum,MachineItemName)");

                sb.Append("VALUES('");
                sb.Append(model.SysCode);
                sb.Append("','");
                sb.Append(model.ResultType);
                sb.Append("','");
                sb.Append(model.CheckNo);
                sb.Append("','");
                sb.Append(model.StdCode);
                sb.Append("','");
                sb.Append(model.SampleCode);
                sb.Append("','");
                sb.Append(model.CheckedCompany);
                sb.Append("','");
                sb.Append(model.CheckedCompanyName);
                sb.Append("','");
                sb.Append(model.CheckedComDis);
                sb.Append("','");
                sb.Append(model.CheckPlaceCode);
                sb.Append("','");
                sb.Append(model.FoodCode);
                sb.Append("','");

                if (model.ProduceDate != null)
                {
                    sb.Append(model.ProduceDate);
                    sb.Append("','");
                }
                //else
                //{
                //    sb.Append("");
                //}
                //sb.Append("','");
                sb.Append(model.ProduceCompany);
                sb.Append("','");
                sb.Append(model.ProducePlace);
                sb.Append("','");
                sb.Append(model.SentCompany);
                sb.Append("','");
                sb.Append(model.Provider);
                sb.Append("','");
                sb.Append(model.TakeDate);
                sb.Append("','");
                sb.Append(model.CheckStartDate);
                sb.Append("','");
                sb.Append(model.CheckEndDate);
                sb.Append("','");
                sb.Append(model.ImportNum);
                sb.Append("','");
                sb.Append(model.SaveNum);
                sb.Append("','");
                sb.Append(model.Unit);
                sb.Append("',");
                sb.Append(model.SampleBaseNum);
                sb.Append(",");
                sb.Append(model.SampleNum);
                sb.Append(",'");
                sb.Append(model.SampleUnit);
                sb.Append("','");
                sb.Append(model.SampleLevel);
                sb.Append("','");
                sb.Append(model.SampleModel);
                sb.Append("','");
                sb.Append(model.SampleState);
                sb.Append("','");
                sb.Append(model.Standard);
                sb.Append("','");
                sb.Append(model.CheckMachine);
                sb.Append("','");
                sb.Append(model.CheckTotalItem);
                sb.Append("','");
                sb.Append(model.CheckValueInfo);
                sb.Append("','");
                sb.Append(model.StandValue);
                sb.Append("','");
                sb.Append(model.Result);
                sb.Append("','");
                sb.Append(model.ResultInfo);
                sb.Append("','");
                sb.Append(model.UpperCompany);
                sb.Append("','");
                sb.Append(model.UpperCompanyName);
                sb.Append("','");
                sb.Append(model.OrCheckNo);
                sb.Append("','");
                sb.Append(model.CheckType);
                sb.Append("','");
                sb.Append(model.CheckUnitCode);
                sb.Append("','");
                sb.Append(model.Checker);
                sb.Append("','");
                sb.Append(model.Assessor);
                sb.Append("','");
                sb.Append(model.Organizer);
                sb.Append("','");
                sb.Append(model.Remark);
                sb.Append("','");
                sb.Append(model.CheckPlanCode);
                sb.Append("',");
                sb.Append(model.SaleNum);
                sb.Append(",");
                sb.Append(model.Price);
                sb.Append(",'");
                sb.Append(model.CheckederVal);
                sb.Append("','");
                sb.Append(model.IsSentCheck);
                sb.Append("','");
                sb.Append(model.CheckederRemark);
                sb.Append("','");
                sb.Append(model.Notes);
                sb.Append("','");

                sb.Append(model.HolesNum);
                sb.Append("','");
                sb.Append(model.MachineSampleNum);
                sb.Append("','");
                sb.Append(model.MachineItemName);
                //sb.Append("','");
                //sb.Append(model.ParentCompanyName );
                sb.Append("')");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);

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
                string sql = string.Format("SELECT syscode FROM tResult WHERE syscode LIKE '{0}' ORDER BY SYSCODE DESC", prevCode );
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
                sb.Length=0;
                sb.Append("UPDATE tResult SET ");
                sb.Append("IsSended=");
                sb.Append(model.IsSended);
                sb.Append(",IsReSended=");
                sb.Append(model.IsReSended);
                sb.Append(",SendedDate='");
                sb.Append(model.SendedDate);
                sb.Append("',Sender='");
                sb.Append(model.Sender);
                sb.Append("'");
                sb.Append(" WHERE SysCode='");
                sb.Append(model.SysCode);
                sb.Append("'");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;

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
		public static bool ExeAllReSend(string strWhere,out string errMsg)
		{
			errMsg=string.Empty; 
			bool rtn=false;			

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

				if(!strWhere.Equals(""))
				{
                    sb.Append(" WHERE A.ISSENDED=TRUE AND ");
                    sb.Append(strWhere);
				}
                string querySql = string.Format("UPDATE tResult SET ISSENDED=FALSE,SENDER=NULL,SENDEDDATE=NULL,ISRESENDED=TRUE WHERE SYSCODE IN (SELECT SysCode FROM ({0}) AS NewTable) ", sb.ToString());

               
				bool obj=DataBase.ExecuteCommand(querySql,out errMsg);
                sb.Length = 0;
				rtn=obj;
			}
			catch(Exception e)
			{
				//Log.WriteLog("更新clsResult",e);
				errMsg=e.Message;
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
                sb.Length=0;

                sb.Append("SELECT A.CheckedCompany & Year(Date()) & A.CheckNo AS CheckSheetNO,");

                sb.Append("(SELECT Name FROM tFoodClass WHERE A.FoodCode=tFoodClass.SysCode) AS FoodName,");
                sb.Append("A.SampleModel,A.SampleState,A.ProduceInfo,A.SampleNum,A.TakeDate,B.Address,");
                sb.Append("(SELECT Name FROM tUserInfo WHERE tUserInfo.UserCode=A.Checker) AS Checker,");
                sb.Append("(SELECT FullName FROM tCompany WHERE A.CheckedCompany=tCompany.SysCode) AS ComName,");
                sb.Append("(SELECT ItemDes FROM tCheckItem WHERE A.CheckTotalItem=tCheckItem.SysCode) AS CheckItem,");
                sb.Append("(SELECT StandardValue FROM tCheckItem WHERE A.CheckTotalItem=tCheckItem.SysCode) AS StandardValue,");
                sb.Append("A.CheckValueInfo,A.ResultInfo,A.CheckStartDate,A.Result,A.Remark");
                sb.Append(" FROM tResult AS A LEFT JOIN tCompany AS B on A.CheckedCompany=B.SysCode");

                if (!whereSql.Equals(""))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                }
                if (!orderBySql.Equals(""))
                {
                    sb.Append(" ORDER BY ");
                    sb.Append(orderBySql);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "Result" };

                dtbl = DataBase.GetDataSet(cmd, names, out sErrMsg).Tables["Result"];
                sb.Length = 0;
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
			string errMsg=string.Empty; 
			DataTable dtbl=null;
            sb.Length = 0;
			try
			{
				sb.Append("SELECT");
				sb.Append(" (select ShortName from tUserUnit where sysCode='"  + "') As ShortName,");
				sb.Append("A.CheckNo,B.FullName As ComName,B.Address,(select Name from tCompanyKind where tCompanyKind.sysCode=B.KindCode) As CompanyKind,");
				sb.Append("B.CompanyID,B.PostCode,B.LinkMan,B.LinkInfo,B.OtherCodeInfo,(select Name from tFoodClass where A.FoodCode=tFoodClass.SysCode) As FoodName,");
				sb.Append("A.CheckType,A.SampleModel,A.SampleLevel,");
				sb.Append("A.SampleState,A.Provider,A.SampleNum,A.SampleBaseNum,A.ImportNum,A.SentCompany,A.SaveNum,A.Remark,A.TakeDate,");
				sb.Append("(select Name from tUserInfo where tUserInfo.UserCode=A.Checker) as Checker,");
				sb.Append("(select ItemDes from tCheckItem where A.CheckTotalItem=tCheckItem.SysCode) as CheckItem,");
				sb.Append("(select StdDes from tStandard where A.Standard=tStandard.SysCode) As ReferStandard,");
				sb.Append("A.StandValue as StandardValue,");
				sb.Append("A.CheckValueInfo,A.ResultInfo,A.SampleCode,A.Result,A.CheckStartDate,");
				sb.Append("(select Name from tUserInfo where tUserInfo.UserCode=A.Assessor) as Assessor");
                sb.Append(" from tResult As A left join tCompany As B on A.CheckedCompany=B.SysCode");

				if(!whereSql.Equals(""))
				{
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
				}
				if(!orderBySql.Equals(""))
				{
                    sb.Append(" ORDER BY ");
                    sb.Append(orderBySql);
				}
                string[] cmd = new string[1] { sb.ToString() };
				string[] names=new string[1]{"Result"};
				dtbl=DataBase.GetDataSet(cmd,names,out errMsg).Tables["Result"];
                sb.Length = 0;
			}
			catch(Exception e)
			{
				//				Log.WriteLog("查询clsResult",e);
				errMsg=e.Message;
			}

			return dtbl;
		}

		public DataTable GetDataTable_ReportGZ(string whereSql, string orderBySql)
		{
			string errMsg=string.Empty; 
			DataTable dt=null;
            sb.Length = 0;
			try
			{
				sb.Append("SELECT");
				sb.AppendFormat(" (SELECT ShortName from tUserUnit where sysCode='{0}') As ShortName,A.CheckNo,","");
				sb.Append("(select Name from tFoodClass where A.FoodCode=tFoodClass.SysCode) As FoodName,A.Provider,A.SampleModel,(select StdDes from tStandard where A.Standard=tStandard.SysCode) As ReferStandard,");
				sb.Append("A.ProduceDate,A.Price,A.ImportNum+A.Unit As ImportNumUnit,A.SaveNum+A.Unit As SaveNumUnit,(select FullName from tCompany where A.ProduceCompany=tCompany.SysCode And tCompany.Property='生产单位') As ProduceCompanyName,");
				sb.Append("(select LinkMan from tCompany where A.ProduceCompany=tCompany.SysCode And tCompany.Property='生产单位') As ProduceLinkMan,B.CompanyID,B.Incorporator,B.LinkInfo,B.PostCode,B.FullName As ComName,B.Address,");
				sb.Append("A.SampleState,A.Provider,A.SampleNum,A.SampleBaseNum,A.ImportNum,A.SentCompany,A.SaveNum,A.Remark,A.TakeDate,");
				sb.Append("(select Name from tDistrict where B.DistrictCode=tDistrict.SysCode) As DistrictName,(select Name from tCompanyKind where B.KindCode=tCompanyKind.SysCode) As KindName,");
				sb.Append("B.ComProperty,(select FullName from tUserUnit where sysCode='0001') As CheckUnitFullName,(select LinkMan from tUserUnit where sysCode='0001') As CheckUnitLinkMan,");
				sb.Append("(select ItemDes from tCheckItem where A.CheckTotalItem=tCheckItem.SysCode) as CheckItem,(select MachineName from tMachine where A.CheckMachine=tMachine.SysCode) as MachineName,A.TakeDate,str(A.SampleNum)+A.SampleUnit As SampleNumUnit,");
				sb.Append("str(A.SampleBaseNum)+A.SampleUnit As SampleBaseNumUnit,A.StandValue+A.ResultInfo As StandValueInfo,A.CheckValueInfo+A.ResultInfo As CheckValueInfo,A.Result,A.CheckederVal,A.IsSentCheck,A.CheckederRemark,A.Remark ");
				sb.Append(" from tResult As A left join tCompany As B on A.CheckedCompany=B.SysCode");

				if(!whereSql.Equals(""))
				{
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
				}
				if(!orderBySql.Equals(""))
				{
                    sb.Append(" ORDER BY ");
                    sb.Append(orderBySql);
				}
			   string[] cmd = new string[1] { sb.ToString() };
				string[] names=new string[1]{"Result"};
				dt=DataBase.GetDataSet(cmd,names,out errMsg).Tables["Result"];
                sb.Length=0;
			}
			catch(Exception e)
			{
				//Log.WriteLog("查询clsResult",e);
				errMsg=e.Message;
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
                sb.Length=0;
                sb.Append("SELECT COUNT(*) FROM TRESULT");
                if (!strWhere.Equals(""))
                {
                    sb.Append(" WHERE ");
                    sb.Append(strWhere);
                }
                Object obj = DataBase.GetOneValue(sb.ToString(), out errMsg);
                sb.Length = 0;
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
                        if (comstdcode.Length ==7)
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
