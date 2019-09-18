using System;
using System.Data;
using System.Text;
using DY.FoodClientLib.Model;

namespace DY.FoodClientLib
{
	/// <summary>
	///检测结果操作类
	/// </summary>
	public class clsResultOpr
	{
		public clsResultOpr()
		{

		}

        private StringBuilder _strBd = new StringBuilder();

        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <param name="errMsg"></param>
        public void DeleteAll(out string errMsg)
        {
            _strBd.Length = 0;
            _strBd.Append("DELETE FROM tResult");
            DataBase.ExecuteCommand(_strBd.ToString(),out errMsg);
            _strBd.Length = 0;
        }


        /// <summary>
        /// 部分修改保存
        /// </summary>
        /// <param name="model">对象clsResult的一个实例参数</param>
        /// <returns></returns>
        public int UpdatePartReport(clsResult model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("UPDATE tResult SET ");
                _strBd.AppendFormat("IsReport='{0}'", model.IsReport);
                _strBd.AppendFormat(" WHERE SysCode='{0}'", model.SysCode);
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
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
		/// 部分修改保存
		/// </summary>
		/// <param name="model">对象clsResult的一个实例参数</param>
		/// <returns></returns>
		public int UpdatePart(clsResult model,out string errMsg)
		{
			errMsg=string.Empty; 
			int rtn=0;
            _strBd.Length=0;
            try
            {
                _strBd.Append("UPDATE tResult SET ");
                _strBd.AppendFormat("CheckNo='{0}',", model.CheckNo);
                _strBd.AppendFormat("StdCode='{0}',", model.StdCode);
                _strBd.AppendFormat("SampleCode='{0}',", model.SampleCode);
                _strBd.AppendFormat("CheckedCompany='{0}',", model.CheckedCompany);
                _strBd.AppendFormat("CheckedCompanyName='{0}',", model.CheckedCompanyName);
                _strBd.AppendFormat("CheckedComDis='{0}',", model.CheckedComDis);
                _strBd.AppendFormat("CheckPlaceCode='{0}',", model.CheckPlaceCode);
                _strBd.AppendFormat("FoodCode='{0}',", model.FoodCode);
                if (model.ProduceDate != null)
                {
                    _strBd.AppendFormat("ProduceDate='{0}',", model.ProduceDate);
                }
                if (model.ProduceDate == null)
                {
                    _strBd.AppendFormat("ProduceDate=null,", "");
                }
                _strBd.AppendFormat("ProduceCompany='{0}',", model.ProduceCompany);
                _strBd.AppendFormat("ProducePlace='{0}',", model.ProducePlace);
                _strBd.AppendFormat("SentCompany='{0}',", model.SentCompany);
                _strBd.AppendFormat("Provider='{0}',", model.Provider);
                _strBd.AppendFormat("TakeDate='{0}',", model.TakeDate);
                _strBd.AppendFormat("CheckStartDate='{0}',", model.CheckStartDate);
                _strBd.AppendFormat("CheckEndDate='{0}',", model.CheckEndDate);
                _strBd.AppendFormat("ImportNum='{0}',", model.ImportNum);
                _strBd.AppendFormat("SaveNum='{0}',", model.SaveNum);
                _strBd.AppendFormat("Unit='{0}',", model.Unit);
                if (model.SampleBaseNum != "null")
                {
                    _strBd.AppendFormat("SampleBaseNum={0},", model.SampleBaseNum);
                }
                if (model.SaleNum != "null")
                {
                    _strBd.AppendFormat("SampleNum={0},", model.SampleNum);
                }
                _strBd.AppendFormat("SampleUnit='{0}',", model.SampleUnit);
                _strBd.AppendFormat("SampleLevel='{0}',", model.SampleLevel);
                _strBd.AppendFormat("SampleModel='{0}',", model.SampleModel);
                _strBd.AppendFormat("SampleState='{0}',", model.SampleState);
                _strBd.AppendFormat("Standard='{0}',", model.Standard);
                _strBd.AppendFormat("CheckMachine='{0}',", model.CheckMachine);
                _strBd.AppendFormat("CheckTotalItem='{0}',", model.CheckTotalItem);
                _strBd.AppendFormat("CheckValueInfo='{0}',", model.CheckValueInfo);
                _strBd.AppendFormat("StandValue='{0}',", model.StandValue);
                _strBd.AppendFormat("Result='{0}',", model.Result);
                _strBd.AppendFormat("ResultInfo='{0}',", model.ResultInfo);
                _strBd.AppendFormat("UpperCompany='{0}',", model.UpperCompany);
                _strBd.AppendFormat("UpperCompanyName='{0}',", model.UpperCompanyName);
                _strBd.AppendFormat("OrCheckNo='{0}',", model.OrCheckNo);
                _strBd.AppendFormat("CheckType='{0}',", model.CheckType);
                _strBd.AppendFormat("CheckUnitCode='{0}',", model.CheckUnitCode);
                _strBd.AppendFormat("Checker='{0}',", model.Checker);
                _strBd.AppendFormat("Assessor='{0}',", model.Assessor);
                _strBd.AppendFormat("Organizer='{0}',", model.Organizer);
                _strBd.AppendFormat("Remark='{0}',", model.Remark);
                _strBd.AppendFormat("CheckPlanCode='{0}',", model.CheckPlanCode);
                if (model.SaleNum != "null")
                {
                    _strBd.AppendFormat("SaleNum={0},", model.SaleNum);
                }
                if (model.Price != "null")
                {
                    _strBd.AppendFormat("Price={0},", model.Price);
                }
                _strBd.AppendFormat("CheckederVal='{0}',", model.CheckederVal);
                _strBd.AppendFormat("IsSentCheck='{0}',", model.IsSentCheck);
                _strBd.AppendFormat("CheckederRemark='{0}',", model.CheckederRemark);
                //sb.AppendFormat("ParentCompanyName='{0}',",model .ParentCompanyName );
                _strBd.AppendFormat("Notes='{0}'", model.Notes);
                _strBd.AppendFormat(" WHERE SysCode='{0}'", model.SysCode);
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);

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
            _strBd.Length=0;
            _strBd.Append("SELECT COUNT(1) FROM tResult ");
            if (strWhere.Length > 0)
            {
                _strBd.Append(" WHERE ");
                _strBd.Append(strWhere);
            }
            object obj = DataBase.GetOneValue(_strBd.ToString(), out errMsg);
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
        /// 根据报表ID删除子表样品信息
        /// </summary>
        /// <param name="ReportID">报表ID</param>
        /// <param name="sErr">异常信息</param>
        /// <returns>return</returns>
        public string DeletedReportDetailByID(string ReportID, string type, out string sErr)
        {
            sErr = string.Empty;
            try
            {
                _strBd.Length = 0;
                if (type.Equals("MZYJ"))
                {
                    _strBd.AppendFormat("Delete From tReportDetail Where ReportID = '{0}'", ReportID);
                }
                else if (type.Equals("SZ"))
                {
                    _strBd.AppendFormat("Delete From tReportDetailSZ Where ReportID = '{0}'", ReportID);
                }
                else
                {
                    _strBd.AppendFormat("Delete From tReportDetail Where ReportID = '{0}'", ReportID);
                }
                DataBase.ExecuteCommand(_strBd.ToString(), out sErr);
                _strBd.Length = 0;
            }
            catch (Exception ex)
            {
                sErr = ex.Message;
            }
            return sErr;
        }

        /// <summary>
        /// 根据ID删除报表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="sErr"></param>
        /// <param name="type">SZ 深圳；MZYJ 每周一检</param>
        /// <returns></returns>
        public string DeletedReportByID(string ID, out string sErr, string type)
        {
            sErr = string.Empty;
            _strBd.Length = 0;
            try
            {
                if (type.Equals("SZ"))
                {
                    _strBd.AppendFormat("Delete From tReportSZ Where ID = {0}", ID);
                }
                else if (type.Equals("MZYJ"))
                {
                    _strBd.AppendFormat("Delete From tReport Where ID = '{0}'", ID);
                }
                DataBase.ExecuteCommand(_strBd.ToString(), out sErr);
            }
            catch (Exception ex)
            {
                sErr = ex.Message;
            }
            return sErr;
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
            _strBd.Length=0;
            try
            {
                _strBd.AppendFormat("DELETE FROM tResult WHERE SysCode='{0}'", mainkey);
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);

                rtn = 1;
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
        public DataTable SearchReportDetail(string whereSql, string type)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            _strBd.Length = 0;
            try
            {
                if (type.Equals("SZ"))
                {
                    _strBd.Append("Select * From tReportDetailSZ ");
                }
                else if (type.Equals("MZYJ"))
                {
                    _strBd.Append("Select * From tReportDetail ");
                }
                if (!whereSql.Equals(""))
                {
                    _strBd.Append(" Where ReportID = ");
                    _strBd.Append(whereSql);
                }
                _strBd.Append(" Order By ID Desc");
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "tReportDetail" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["tReportDetail"];
                _strBd.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 查询报表 - 深圳
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public DataTable SearchReport_SZ(string whereSql)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("Select * From tReportSZ ");
                if (!whereSql.Equals(""))
                {
                    _strBd.Append(" Where ");
                    _strBd.Append(whereSql);
                }
                _strBd.Append(" Order By ID Desc");
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "ReportSZ" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ReportSZ"];
                _strBd.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 根据查询串条件查询记录 - 每周一检
        /// </summary>
        /// <param name="whereSql">查询条件串,不含Where</param>
        /// <param name="orderBySql">排序串,不含Order</param>
        /// <returns></returns>
        public DataTable SearchReport_MZYJ(string whereSql)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            _strBd.Length = 0;
            try
            {
                //sb.Append("Select ReportName,SysCode,CheckItem,CheckedCompany,ProductName,Specifications,BatchNumber,ProductPrice,StandardValues,CheckValues,BusinessLicense,Address,BusinessNature,Contact,ContactPhone,Fax,ZipCode,QualityGrade,RegisteredTrademark,SamplingNumber,SamplingBase,IntoNumber,Implementation,InventoryNubmer,Notes,SamplingData,SamplingPerson,CheckBasis,SamplingCode,Conclusion,NoteUnder,CheckData,CheckUser,ApprovedUser From tReport ");
                _strBd.Append("Select * From tReport ");
                if (!whereSql.Equals(""))
                {
                    _strBd.Append(" Where ");
                    _strBd.Append(whereSql);
                }
                _strBd.Append(" Order By CreateDate Desc");
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "Report" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Report"];
                _strBd.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 根据条件查询检测记录信息
        /// 2016年1月15日
        /// wenj
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public DataTable SearchResult(string whereSql)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("Select * From tResult");
                if (!whereSql.Equals(""))
                {
                    _strBd.Append(" Where ");
                    _strBd.Append(whereSql);
                }
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                _strBd.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
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
            _strBd.Length=0;
            try
            {
                _strBd.Append("SELECT ");//新两个字段
                _strBd.Append("A.IsSended,A.SysCode, A.CheckNo,A.SampleCode,A.CheckPlanCode, A.CheckedCompany, A.CheckedCompanyName,");
                _strBd.Append("A.CheckedComDis, A.UpperCompany,A.UpperCompanyName, A.FoodCode, A.FoodName,");
                _strBd.Append("A.CheckType, A.SampleModel, A.SampleLevel, A.SampleState, A.Provider, ");
                _strBd.Append("A.StdCode, A.OrCheckNo, A.ProduceCompany, A.ProduceCompanyName,A.ProducePlace,A.ProducePlaceName,");
                _strBd.Append("A.ProduceDate, A.ImportNum,A.SaleNum, A.SaveNum, A.Price,A.Unit, A.SampleNum, A.SampleBaseNum,");
                _strBd.Append("A.SampleUnit, A.SentCompany, A.Remark,A.CheckederVal,A.CheckederRemark,A.Notes, ");
                _strBd.Append("format(A.TakeDate,\"yyyy-mm-dd\")AS TakeDate,");
                _strBd.Append("A.OrganizerName, A.CheckTotalItem, A.CheckTotalItemName, A.Standard, A.StandardName,");
                _strBd.Append("A.CheckValueInfo,A.ResultInfo,A.StandValue, A.Result,A.IsSentCheck,");
                 _strBd.Append("Format$(A.CheckStartDate,\"General Date\")AS CheckStartDate,");
                _strBd.Append("A.Checker, A.CheckerName, A.Assessor, A.AssessorName,");
                _strBd.Append("A.CheckUnitCode,A.CheckUnitName,A.CheckUnitInfo,"); //新增加
                _strBd.Append("A.ResultType,A.HolesNum,A.MachineSampleNum, ");
                _strBd.Append("A.MachineName,A.MachineModel,A.MachineCompany, A.SendedDate,");
                _strBd.Append("A.Sender,P.Name AS SenderName,A.IsReSended, A.CheckPlaceCode, A.CheckPlace,");
                _strBd.Append("A.CheckEndDate, A.CheckMachine,A.Organizer ");

                _strBd.Append(" FROM [SELECT T11.*,O.ItemDes As CheckTotalItemName");
                _strBd.Append(" FROM (SELECT T10.*,S.StdDes As StandardName");
                _strBd.Append(" FROM (SELECT T8.*,Q.Name As ProducePlaceName");
                _strBd.Append(" FROM (SELECT T6.*,N.Name As OrganizerName");
                _strBd.Append(" FROM (SELECT T5.*,M.Name As AssessorName");
                _strBd.Append(" FROM (SELECT T4.*,L.FullName As ProduceCompanyName");
                _strBd.Append(" FROM (SELECT T3.*,K.MachineName,K.MachineModel,K.Company AS MachineCompany");//新增加
                _strBd.Append(" FROM (SELECT T2.*,J.Name AS CheckPlace");
                _strBd.Append(" FROM (SELECT C.*,B.Name AS FoodName,I.Name AS CheckerName,");
                _strBd.Append(" H.FullName AS CheckUnitName,H.ShortName AS CheckUnitInfo"); //新增加
                _strBd.Append(" FROM tResult AS C,tFoodClass AS B,tUserUnit AS H, tUserInfo AS I");
                _strBd.Append(" WHERE C.FoodCode=B.SysCode AND C.CheckUnitCode=H.SysCode AND C.Checker=I.UserCode)AS T2");
                _strBd.Append(" LEFT JOIN tDistrict AS J On T2.CheckPlaceCode =J.SysCode)AS T3");
                _strBd.Append(" LEFT JOIN tMachine AS K On T3.CheckMachine=K.SysCode)AS T4");
                _strBd.Append(" LEFT JOIN tCompany AS L On T4.ProduceCompany=L.SysCode)AS T5");
                _strBd.Append(" LEFT JOIN tUserInfo AS M On T5.Assessor=M.UserCode)As T6");
                _strBd.Append(" LEFT JOIN tUserInfo AS N On T6.Organizer=N.UserCode)As T8");
                _strBd.Append(" LEFT JOIN tProduceArea AS Q On T8.ProducePlace=Q.SysCode)As T10");
                _strBd.Append(" LEFT JOIN tStandard AS S On T10.Standard=S.SysCode)AS T11");
                _strBd.Append(" LEFT JOIN tCheckItem AS O On T11.CheckTotalItem=O.SysCode]. AS A");
                _strBd.Append(" LEFT JOIN tUserInfo AS P ON A.Sender=P.UserCode ");

                if (!whereSql.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(whereSql);
                }
                if (!orderBySql.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderBySql);
                }
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "Result" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                _strBd.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsResult",e);
                errMsg = e.Message;
            }

            return dtbl;
        }

        public DataTable GetAsDataTable_AnHui(string whereSql, string orderBySql)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT ");//新两个字段
                _strBd.Append("A.fTpye,A.IsSended,A.SysCode,A.CheckNo,A.SampleCode,A.CheckPlanCode, A.CheckedCompany, A.CheckedCompanyName,");
                _strBd.Append("A.CheckedComDis, A.UpperCompany,A.UpperCompanyName, A.FoodCode, A.FoodName,");
                _strBd.Append("A.CheckType, A.SampleModel, A.SampleLevel, A.SampleState, A.Provider, ");
                _strBd.Append("A.StdCode, A.OrCheckNo, A.ProduceCompany, A.ProduceCompanyName,A.ProducePlace,A.ProducePlaceName,");
                _strBd.Append("A.ProduceDate, A.ImportNum,A.SaleNum, A.SaveNum, A.Price,A.Unit, A.SampleNum, A.SampleBaseNum,");
                _strBd.Append("A.SampleUnit, A.SentCompany, A.Remark,A.CheckederVal,A.CheckederRemark,A.Notes, ");
                _strBd.Append("format(A.TakeDate,\"yyyy-mm-dd\")AS TakeDate,");
                _strBd.Append("A.OrganizerName, A.CheckTotalItem, A.CheckTotalItemName, A.Standard, A.StandardName,");
                _strBd.Append("A.CheckValueInfo,A.ResultInfo,A.StandValue, A.Result,A.IsSentCheck,");
                _strBd.Append("Format$(A.CheckStartDate,\"General Date\")AS CheckStartDate,");
                _strBd.Append("A.Checker, A.CheckerName, A.Assessor, A.AssessorName,");
                _strBd.Append("A.CheckUnitCode,A.CheckUnitName,A.CheckUnitInfo,"); //新增加
                _strBd.Append("A.ResultType,A.HolesNum,A.MachineSampleNum, ");
                _strBd.Append("A.MachineName,A.MachineModel,A.MachineCompany, A.SendedDate,");
                _strBd.Append("A.Sender,P.Name AS SenderName,A.IsReSended, A.CheckPlaceCode, A.CheckPlace,");
                _strBd.Append("A.CheckEndDate, A.CheckMachine,A.Organizer ");

                _strBd.Append(" FROM [SELECT T11.*,O.ItemDes As CheckTotalItemName");
                _strBd.Append(" FROM (SELECT T10.*,S.StdDes As StandardName");
                _strBd.Append(" FROM (SELECT T8.*,Q.Name As ProducePlaceName");
                _strBd.Append(" FROM (SELECT T6.*,N.Name As OrganizerName");
                _strBd.Append(" FROM (SELECT T5.*,M.Name As AssessorName");
                _strBd.Append(" FROM (SELECT T4.*,L.FullName As ProduceCompanyName");
                _strBd.Append(" FROM (SELECT T3.*,K.MachineName,K.MachineModel,K.Company AS MachineCompany");//新增加
                _strBd.Append(" FROM (SELECT T2.*,J.Name AS CheckPlace");
                _strBd.Append(" FROM (SELECT C.*,B.Name AS FoodName,I.Name AS CheckerName,");
                _strBd.Append(" H.FullName AS CheckUnitName,H.ShortName AS CheckUnitInfo"); //新增加
                _strBd.Append(" FROM tResult AS C,tFoodClass AS B,tUserUnit AS H, tUserInfo AS I");
                _strBd.Append(" WHERE C.FoodCode=B.SysCode AND C.CheckUnitCode=H.SysCode AND C.Checker=I.UserCode)AS T2");
                _strBd.Append(" LEFT JOIN tDistrict AS J On T2.CheckPlaceCode =J.SysCode)AS T3");
                _strBd.Append(" LEFT JOIN tMachine AS K On T3.CheckMachine=K.SysCode)AS T4");
                _strBd.Append(" LEFT JOIN tCompany AS L On T4.ProduceCompany=L.SysCode)AS T5");
                _strBd.Append(" LEFT JOIN tUserInfo AS M On T5.Assessor=M.UserCode)As T6");
                _strBd.Append(" LEFT JOIN tUserInfo AS N On T6.Organizer=N.UserCode)As T8");
                _strBd.Append(" LEFT JOIN tProduceArea AS Q On T8.ProducePlace=Q.SysCode)As T10");
                _strBd.Append(" LEFT JOIN tStandard AS S On T10.Standard=S.SysCode)AS T11");
                _strBd.Append(" LEFT JOIN tCheckItem AS O On T11.CheckTotalItem=O.SysCode]. AS A");
                _strBd.Append(" LEFT JOIN tUserInfo AS P ON A.Sender=P.UserCode ");

                if (!whereSql.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(whereSql);
                }
                if (!orderBySql.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderBySql);
                }
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "Result" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                _strBd.Length = 0;
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
            _strBd.Length=0;
            try
            {
                _strBd.Append("SELECT ");//新两个字段
                //新增安徽项目接口字段
                _strBd.Append("A.fTpye as fTpye,A.testPro as testPro,A.quanOrQual as quanOrQual,A.dataNum as dataNum,A.checkedUnit as checkedUnit,");

                _strBd.Append("A.SysCode,A.ResultType, A.CheckNo,A.SampleCode,A.StdCode,A.MachineItemName,A.SampleCode,A.CheckUnitCode,");
                _strBd.Append("A.CheckedCompany AS CheckedCompanyCode,");//企业编号
                //如是是工商版/农检片本是两级 要查询"UpperCompanyName--CheckedCompany"
                //如果是食药 餐饮版要查询"CheckedCompanyName--CheckedCompany"
                _strBd.Append("A.UpperCompanyName AS CheckedCompany, ");
                //if (appTag == ShareOption.FDAppTag)//食药为一级就够了
                //    _strBd.Append("A.UpperCompanyName AS CheckedCompany, ");
                //else
                //    _strBd.Append("A.UpperCompanyName As CheckedCompany,");
                _strBd.Append("A.CheckedCompanyName AS CheckedCompanyInfo,A.CheckedComDis,");
                _strBd.Append("A.CheckPlaceCode,");
                _strBd.Append("(SELECT J.Name FROM tDistrict AS J WHERE A.CheckPlaceCode =J.SysCode)AS CheckPlace,");//检测机构名称
                _strBd.Append("(select z.Cdcode from tProprietors as z where z.Cdname=A.CheckedCompanyName and z.Ciname=A.UpperCompanyName)as CheckedDealerCode,");//被检经营户编号
                _strBd.Append("(SELECT B.Name FROM tFoodClass AS B WHERE A.FoodCode=B.SysCode)AS FoodName,");//查询食品名称 A.FoodCode,
                _strBd.Append("(SELECT B.Name FROM tFoodClass AS B WHERE A.fTpye=B.SysCode)AS FoodTypeName,");//查询食品种类名称 A.FoodCode,
                _strBd.Append("A.SentCompany,A.Provider,A.ProduceDate,");
                _strBd.Append("(SELECT L.FullName FROM tCompany AS L WHERE A.ProduceCompany=L.SysCode)AS ProduceCompany,");//A.ProduceCompanyName 
                _strBd.Append("(SELECT Q.Name FROM tProduceArea AS Q WHERE Q.SysCode=A.ProducePlace)AS ProducePlace,");//查询产地名称A.ProducePlace,
                //区域目前只构造五级
                _strBd.Append("( IIf(Len(A.ProducePlace)=6,(SELECT R.Name FROM tProduceArea AS R WHERE R.SysCode=MID(A.ProducePlace,1,6)),'')+ IIf(Len(A.ProducePlace)>6,(SELECT R.Name FROM tProduceArea AS R WHERE R.SysCode=MID(A.ProducePlace,1,9))+'/','') + IIf(Len(A.ProducePlace)>9,(SELECT R.Name FROM tProduceArea AS R WHERE R.SysCode=MID(A.ProducePlace,1,12))+'/','') + IIf(Len(A.ProducePlace)>12,(SELECT R.Name FROM tProduceArea AS R WHERE R.SysCode=MID(A.ProducePlace,1,15))+'/','')+ IIf(Len(A.ProducePlace)>15,(SELECT R.Name FROM tProduceArea AS R WHERE R.SysCode=MID(A.ProducePlace,1,18))+'/','')+IIf(Len(A.ProducePlace)>18,(SELECT R.Name FROM tProduceArea AS R WHERE R.SysCode=MID(A.ProducePlace,1,21))+'/','') ) AS ProducePlaceInfo,"); //ProducePlaceInfo 需要这个字段
                _strBd.Append("A.ImportNum,A.SaleNum, A.SaveNum, A.Price,A.SampleNum, A.SampleBaseNum,");
                _strBd.Append("A.TakeDate,");
                _strBd.Append(" format(A.CheckStartDate,'yyyy-MM-dd HH:mm:ss') as CheckStartDate,A.CheckEndDate,");
                _strBd.Append("A.Unit,A.SampleUnit,A.SampleModel, A.SampleLevel,A.SampleState,");
                _strBd.Append("(SELECT S.StdDes FROM tStandard AS S WHERE A.Standard=S.SysCode)AS Standard,");
                _strBd.Append("K.MachineName AS CheckMachine,K.MachineModel AS CheckMachineModel,K.Company AS MachineCompany,");
                _strBd.Append("(SELECT O.ItemDes FROM tCheckItem AS O WHERE O.SysCode=A.CheckTotalItem)AS CheckTotalItem,");//查询检测项目名称 
                _strBd.Append("A.CheckValueInfo,A.StandValue, A.Result,A.ResultInfo,A.OrCheckNo,");
                _strBd.Append("A.UpperCompany,A.UpperCompanyName,");
                _strBd.Append("A.CheckType,");
                //sb.Append("H.FullName AS CheckUnitName,H.ShortName AS CheckUnitInfo,"); //联合新增加A.CheckUnitCode,
                _strBd.Append("H.FullName AS CheckUnitName,(SELECT I.Name FROM tUserInfo AS I WHERE I.UserCode=A.Checker)  AS CheckUnitInfo,");
                _strBd.Append("(SELECT I.Name FROM tUserInfo AS I WHERE I.UserCode=A.Checker) AS Checker,");//A.Organizer
                _strBd.Append("(SELECT M.Name FROM tUserInfo AS M WHERE M.UserCode=A.Assessor) AS Assessor,");//A.Organizer Assessor
                _strBd.Append("(SELECT N.Name FROM tUserInfo AS N WHERE N.UserCode=A.Organizer) AS Organizer,");//A.Organizer
                _strBd.Append("A.Remark,A.CheckPlanCode,A.CheckederVal,A.IsSentCheck,A.CheckederRemark,");
                _strBd.Append("IIf(A.IsReSended, '是', '否')AS IsReSended,");
                _strBd.Append("'1' AS UpLoader,A.Notes");
                _strBd.Append(" FROM (tResult AS A LEFT JOIN tUserUnit AS H ON A.CheckUnitCode=H.SysCode)");//联合检测单位
                _strBd.Append(" LEFT JOIN tMachine AS K On A.CheckMachine=K.SysCode");
  
                if (!strWhere.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(strWhere);
                }
                if (!orderBy.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderBy);
                }
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "Result" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                _strBd.Length = 0;
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
            _strBd.Length=0;
            try
            {
                _strBd.Append("SELECT A.IsSended, A.SendedDate, A.Sender, P.Name AS SenderName,");
                _strBd.Append("A.IsReSended,A.SysCode, A.CheckNo,A.CheckPlanCode, A.CheckedCompany,");
                _strBd.Append("A.CheckedCompanyName,A.CheckedComDis, A.UpperCompany,A.UpperCompanyName,A.FoodCode, A.FoodName,"); 
                _strBd.Append("A.CheckType, A.SampleModel, A.SampleLevel, A.SampleState, A.Provider, A.StdCode, A.OrCheckNo,");
                _strBd.Append("A.ProduceCompany, A.ProduceCompanyName,A.ProducePlace,A.ProducePlaceName, A.ProduceDate,");
                _strBd.Append("A.ImportNum,A.SaleNum, A.SaveNum, A.Price,A.Unit, A.SampleNum, A.SampleBaseNum, A.SampleUnit,");
                _strBd.Append("A.SentCompany, A.Remark,A.CheckederVal,A.CheckederRemark,A.Notes, A.TakeDate, A.OrganizerName,");
                _strBd.Append("A.CheckTotalItem, A.CheckTotalItemName, A.Standard, A.StandardName, "); 
                _strBd.Append("A.CheckValueInfo,A.ResultInfo,A.StandValue,A.SampleCode, A.Result,");
                _strBd.Append("A.IsSentCheck, A.CheckStartDate, A.Checker, A.CheckerName, A.Assessor, A.AssessorName,");
                _strBd.Append("A.CheckUnitCode, A.CheckUnitName,A.ResultType, A.MachineName, A.CheckPlaceCode, A.CheckPlace,");
                _strBd.Append("A.CheckEndDate, A.CheckMachine,  A.Organizer ");

                _strBd.Append(" FROM [SELECT T11.*,O.ItemDes As CheckTotalItemName ");
                _strBd.Append(" FROM (SELECT T10.*,S.StdDes As StandardName");
                _strBd.Append(" FROM (SELECT T8.*,Q.Name As ProducePlaceName");
                _strBd.Append(" FROM (SELECT T6.*,N.Name As OrganizerName");
                _strBd.Append(" FROM (SELECT T5.*,M.Name As AssessorName");
                _strBd.Append(" FROM (SELECT T4.*,L.FullName As ProduceCompanyName");
                _strBd.Append(" FROM (SELECT T3.*,K.MachineName");
                _strBd.Append(" FROM (SELECT T2.*,J.Name AS CheckPlace");
                _strBd.Append(" FROM (SELECT C.*, B.Name AS FoodName,H.FullName AS CheckUnitName,I.Name AS CheckerName");
                _strBd.Append(" FROM tResult AS C, tFoodClass AS B, tUserUnit AS H, tUserInfo AS I");
                _strBd.Append(" WHERE C.FoodCode=B.SysCode And  C.CheckUnitCode=H.SysCode And C.Checker=I.UserCode) As T2");
                _strBd.Append(" LEFT JOIN tDistrict As J On T2.CheckPlaceCode =J.SysCode) As T3");
                _strBd.Append(" LEFT JOIN tMachine As K On T3.CheckMachine=K.SysCode) As T4");
                _strBd.Append(" LEFT JOIN tCompany As L On T4.ProduceCompany=L.SysCode) As T5");
                _strBd.Append(" LEFT JOIN tUserInfo As M On T5.Assessor=M.UserCode) As T6");
                _strBd.Append(" LEFT JOIN tUserInfo As N On T6.Organizer=N.UserCode) As T8");
                _strBd.Append(" LEFT JOIN tProduceArea As Q On T8.ProducePlace=Q.SysCode) As T10");
                _strBd.Append(" LEFT JOIN tStandard As S On T10.Standard=S.SysCode) As T11");
                _strBd.Append(" LEFT JOIN tCheckItem As O On T11.CheckTotalItem=O.SysCode]. AS A");
                _strBd.Append(" LEFT JOIN tUserInfo AS P ON A.Sender=P.UserCode");

                if (!whereSql.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(whereSql);
                }
                if (!orderBySql.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderBySql);
                }
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                _strBd.Length = 0;
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
        /// <param name="model">Report</param>
        /// <returns></returns>
        public int Insert(clsReport.ReportDetail model, out string errMsg)
        {
            _strBd.Length = 0;
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                _strBd.Append("INSERT INTO tReportDetail ");
                _strBd.Append("(ReportID,CheckItem,CheckBasis,StandardValues,CheckValues,Conclusion,CheckData,CheckUser,SysCode,Unit) ");
                _strBd.Append("VALUES('");
                _strBd.Append(model.ReportID);
                _strBd.Append("','");
                _strBd.Append(model.CheckItem);
                _strBd.Append("','");
                _strBd.Append(model.CheckBasis);
                _strBd.Append("','");
                _strBd.Append(model.StandardValues);
                _strBd.Append("','");
                _strBd.Append(model.CheckValues);
                _strBd.Append("','");
                _strBd.Append(model.Conclusion);
                _strBd.Append("','");
                _strBd.Append(model.CheckData);
                _strBd.Append("','");
                _strBd.Append(model.CheckUser);
                _strBd.Append("','");
                _strBd.Append(model.SysCode);
                _strBd.Append("','");
                _strBd.Append(model.Unit);
                _strBd.Append("')");
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 更新报表 - 深圳
        /// 2016年1月19日 wenj
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdateReport(clsReportSZ model, out string errMsg)
        {
            _strBd.Length = 0;
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                _strBd.Append("Update tReportSZ Set ");
                _strBd.AppendFormat("ReportName='{0}',CheckedCompany='{1}',Contact='{2}',ContactPhone='{3}',CheckedCompanyArea='{4}',SamplingData='{5}',",
                    model.ReportName, model.CheckedCompany, model.Contact, model.ContactPhone, model.CheckedCompanyArea, model.SamplingData);
                _strBd.AppendFormat("SamplingPerson='{0}',CheckUser='{1}' ",
                    model.SamplingPerson, model.CheckUser);
                _strBd.AppendFormat("Where ID ={0}", model.ID);
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 更新报表 - 每周一检
        /// 2015年12月14日 wenj
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdateReport(clsReport model, out string errMsg)
        {
            _strBd.Length = 0;
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                _strBd.Append("Update tReport Set ");
                _strBd.AppendFormat("Address='{0}',BusinessNature='{1}',BusinessLicense='{2}',Contact='{3}',ContactPhone='{4}',Fax='{5}',",
                    model.Address, model.BusinessNature, model.BusinessLicense, model.Contact, model.ContactPhone, model.Fax);
                _strBd.AppendFormat("ZipCode='{0}',ProductName='{1}',ProductPrice='{2}',Specifications='{3}',QualityGrade='{4}',BatchNumber='{5}',",
                    model.ZipCode, model.ProductName, model.ProductPrice, model.Specifications, model.QualityGrade, model.BatchNumber);
                _strBd.AppendFormat("RegisteredTrademark='{0}',SamplingNumber='{1}',SamplingBase='{2}',IntoNumber='{3}',Implementation='{4}',InventoryNubmer='{5}',",
                    model.RegisteredTrademark, model.SamplingNumber, model.SamplingBase, model.IntoNumber, model.Implementation, model.InventoryNubmer);
                _strBd.AppendFormat("Notes='{0}',SamplingPerson='{1}',ApprovedUser='{2}',ReportName='{3}'",
                    model.Notes, model.SamplingPerson, model.ApprovedUser, model.ReportName);
                _strBd.AppendFormat("Where ID ='{0}'", model.ID);
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
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
        /// 深圳报表子表
        /// 2016年1月14日
        /// wenj
        /// </summary>
        /// <param name="model">Report</param>
        /// <returns></returns>
        public int Insert(clsReportSZ.clsReportDetailSZ model, out string errMsg)
        {
            _strBd.Length = 0;
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                _strBd.Append("INSERT INTO tReportDetailSZ ");
                _strBd.Append("(ReportID,Code,SampleName,SampleBase,SampleSource,Result,SysCode,");
                _strBd.Append("SampleAmount,SampleNumber,Price,ChekcedValue,StandardValue,Notes,IsDestruction,DestructionKG) ");
                _strBd.Append("VALUES(");

                _strBd.Append(model.ReportID);
                _strBd.Append(",'");
                _strBd.Append(model.Code);
                _strBd.Append("','");
                _strBd.Append(model.SampleName);
                _strBd.Append("','");
                _strBd.Append(model.SampleBase);
                _strBd.Append("','");
                _strBd.Append(model.SampleSource);
                _strBd.Append("','");
                _strBd.Append(model.Result);
                _strBd.Append("','");
                _strBd.Append(model.SysCode);
                _strBd.Append("','");
                _strBd.Append(model.SampleAmount);
                _strBd.Append("','");
                _strBd.Append(model.SampleNumber);
                _strBd.Append("','");
                _strBd.Append(model.Price);
                _strBd.Append("','");
                _strBd.Append(model.ChekcedValue);
                _strBd.Append("','");
                _strBd.Append(model.StandardValue);
                _strBd.Append("','");
                _strBd.Append(model.Note);
                _strBd.Append("','");
                _strBd.Append(model.IsDestruction);
                _strBd.Append("','");
                _strBd.Append(model.DestructionKG);
                _strBd.Append("')");
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
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
        /// 深圳报表
        /// 2016年1月14日
        /// wenj
        /// </summary>
        /// <param name="model">Report</param>
        /// <returns></returns>
        public DataTable Insert(clsReportSZ model, out string errMsg)
        {
            DataTable dt = new DataTable();
            _strBd.Length = 0;
            errMsg = string.Empty;
            try
            {
                _strBd.Append("INSERT INTO tReportSZ ");
                _strBd.Append("(ReportName,CheckedCompany,Contact,ContactPhone,CheckedCompanyArea,");
                _strBd.Append("SamplingData,SamplingPerson,CheckUser) ");
                _strBd.Append("VALUES('");

                _strBd.Append(model.ReportName);
                _strBd.Append("','");
                _strBd.Append(model.CheckedCompany);
                _strBd.Append("','");
                _strBd.Append(model.Contact);
                _strBd.Append("','");
                _strBd.Append(model.ContactPhone);
                _strBd.Append("','");
                _strBd.Append(model.CheckedCompanyArea);
                _strBd.Append("','");

                _strBd.Append(model.SamplingData);
                _strBd.Append("','");
                _strBd.Append(model.SamplingPerson);
                _strBd.Append("','");
                _strBd.Append(model.CheckUser);
                _strBd.Append("')");
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);

                _strBd.Length = 0;
                _strBd.Append("Select Top 1 * From tReportSZ Order By ID Desc");
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                _strBd.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 插入一条明细记录
        /// </summary>
        /// <param name="model">Report</param>
        /// <returns></returns>
        public int Insert(clsReport model, out string errMsg)
        {
            _strBd.Length = 0;
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                _strBd.Append("INSERT INTO tReport ");
                _strBd.Append("(ID,CheckedCompany,Address,BusinessNature,BusinessLicense,");
                _strBd.Append("Contact,ContactPhone,Fax,ZipCode,ProductName,");
                _strBd.Append("ProductPrice,Specifications,QualityGrade,BatchNumber,RegisteredTrademark,");
                _strBd.Append("SamplingNumber,SamplingBase,IntoNumber,Implementation,InventoryNubmer,");
                _strBd.Append("Notes,SamplingData,SamplingPerson,");
                _strBd.Append("SamplingCode,NoteUnder,");
                _strBd.Append("ApprovedUser,ReportName,CreateDate,Unit) ");
                _strBd.Append("VALUES('");

                _strBd.Append(model.ID);
                _strBd.Append("','");
                _strBd.Append(model.CheckedCompany);
                _strBd.Append("','");
                _strBd.Append(model.Address);
                _strBd.Append("','");
                _strBd.Append(model.BusinessNature);
                _strBd.Append("','");
                _strBd.Append(model.BusinessLicense);
                _strBd.Append("','");

                _strBd.Append(model.Contact);
                _strBd.Append("','");
                _strBd.Append(model.ContactPhone);
                _strBd.Append("','");
                _strBd.Append(model.Fax);
                _strBd.Append("','");
                _strBd.Append(model.ZipCode);
                _strBd.Append("','");
                _strBd.Append(model.ProductName);
                _strBd.Append("','");

                _strBd.Append(model.ProductPrice);
                _strBd.Append("','");
                _strBd.Append(model.Specifications);
                _strBd.Append("','");
                _strBd.Append(model.QualityGrade);
                _strBd.Append("','");
                _strBd.Append(model.BatchNumber);
                _strBd.Append("','");
                _strBd.Append(model.RegisteredTrademark);
                _strBd.Append("','");

                _strBd.Append(model.SamplingNumber);
                _strBd.Append("','");
                _strBd.Append(model.SamplingBase);
                _strBd.Append("','");
                _strBd.Append(model.IntoNumber);
                _strBd.Append("','");
                _strBd.Append(model.Implementation);
                _strBd.Append("','");
                _strBd.Append(model.InventoryNubmer);
                _strBd.Append("','");

                _strBd.Append(model.Notes);
                _strBd.Append("','");
                _strBd.Append(model.SamplingData);
                _strBd.Append("','");
                _strBd.Append(model.SamplingPerson);
                _strBd.Append("','");

                _strBd.Append(model.SamplingCode);
                _strBd.Append("','");
                _strBd.Append(model.NoteUnder);
                _strBd.Append("','");

                _strBd.Append(model.ApprovedUser);
                _strBd.Append("','");
                _strBd.Append(model.ReportName);
                _strBd.Append("','");
                _strBd.Append(model.CreateDate);
                _strBd.Append("','");
                _strBd.Append(model.Unit);
                _strBd.Append("')");
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 插入一条明细记录 sz
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertSZ(clsResult model, out string errMsg)
        {
            _strBd.Length = 0;
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                _strBd.Append("INSERT INTO tResult");
                _strBd.Append("(SysCode,ResultType,CheckNo,StdCode,SampleCode,CheckedCompany,");
                _strBd.Append("CheckedCompanyName,CheckedComDis,CheckPlaceCode,FoodCode,");
                if (model.ProduceDate != null)
                    _strBd.Append("ProduceDate,");
                _strBd.Append("ProduceCompany,ProducePlace,SentCompany,Provider,TakeDate,CheckStartDate,CheckEndDate,");
                _strBd.Append("ImportNum,SaveNum,Unit,SampleBaseNum,SampleNum,SampleUnit,SampleLevel,SampleModel,SampleState,");
                _strBd.Append("Standard,CheckMachine,CheckTotalItem,CheckValueInfo,StandValue,Result,ResultInfo,UpperCompany,UpperCompanyName,");
                _strBd.Append("OrCheckNo,CheckType,CheckUnitCode,Checker,Assessor,Organizer,Remark,CheckPlanCode,SaleNum,Price,CheckederVal,");
                _strBd.Append("IsSentCheck,CheckederRemark,Notes, HolesNum,MachineSampleNum,MachineItemName,SampleSource,Contact,ContactPhone,IsDestruction,DestructionKG,Area,SampleAmount)");
                _strBd.Append("VALUES('");
                _strBd.Append(model.SysCode);
                _strBd.Append("','");
                _strBd.Append(model.ResultType);
                _strBd.Append("','");
                _strBd.Append(model.CheckNo);
                _strBd.Append("','");
                _strBd.Append(model.StdCode);
                _strBd.Append("','");
                _strBd.Append(model.SampleCode);
                _strBd.Append("','");
                _strBd.Append(model.CheckedCompany);
                _strBd.Append("','");
                _strBd.Append(model.CheckedCompanyName);
                _strBd.Append("','");
                _strBd.Append(model.CheckedComDis);
                _strBd.Append("','");
                _strBd.Append(model.CheckPlaceCode);
                _strBd.Append("','");
                _strBd.Append(model.FoodCode);
                _strBd.Append("','");
                if (model.ProduceDate != null)
                {
                    _strBd.Append(model.ProduceDate);
                    _strBd.Append("','");
                }
                _strBd.Append(model.ProduceCompany);
                _strBd.Append("','");
                _strBd.Append(model.ProducePlace);
                _strBd.Append("','");
                _strBd.Append(model.SentCompany);
                _strBd.Append("','");
                _strBd.Append(model.Provider);
                _strBd.Append("','");
                _strBd.Append(model.TakeDate);
                _strBd.Append("','");
                _strBd.Append(model.CheckStartDate);
                _strBd.Append("','");
                _strBd.Append(model.CheckEndDate);
                _strBd.Append("','");
                _strBd.Append(model.ImportNum);
                _strBd.Append("','");
                _strBd.Append(model.SaveNum);
                _strBd.Append("','");
                _strBd.Append(model.Unit);
                _strBd.Append("',");
                _strBd.Append(model.SampleBaseNum);
                _strBd.Append(",");
                _strBd.Append(model.SampleNum);
                _strBd.Append(",'");
                _strBd.Append(model.SampleUnit);
                _strBd.Append("','");
                _strBd.Append(model.SampleLevel);
                _strBd.Append("','");
                _strBd.Append(model.SampleModel);
                _strBd.Append("','");
                _strBd.Append(model.SampleState);
                _strBd.Append("','");
                _strBd.Append(model.Standard);
                _strBd.Append("','");
                _strBd.Append(model.CheckMachine);
                _strBd.Append("','");
                _strBd.Append(model.CheckTotalItem);
                _strBd.Append("','");
                _strBd.Append(model.CheckValueInfo);
                _strBd.Append("','");
                _strBd.Append(model.StandValue);
                _strBd.Append("','");
                _strBd.Append(model.Result);
                _strBd.Append("','");
                _strBd.Append(model.ResultInfo);
                _strBd.Append("','");
                _strBd.Append(model.UpperCompany);
                _strBd.Append("','");
                _strBd.Append(model.UpperCompanyName);
                _strBd.Append("','");
                _strBd.Append(model.OrCheckNo);
                _strBd.Append("','");
                _strBd.Append(model.CheckType);
                _strBd.Append("','");
                _strBd.Append(model.CheckUnitCode);
                _strBd.Append("','");
                _strBd.Append(model.Checker);
                _strBd.Append("','");
                _strBd.Append(model.Assessor);
                _strBd.Append("','");
                _strBd.Append(model.Organizer);
                _strBd.Append("','");
                _strBd.Append(model.Remark);
                _strBd.Append("','");
                _strBd.Append(model.CheckPlanCode);
                _strBd.Append("',");
                _strBd.Append(model.SaleNum);
                _strBd.Append(",");
                _strBd.Append(model.Price);
                _strBd.Append(",'");
                _strBd.Append(model.CheckederVal);
                _strBd.Append("','");
                _strBd.Append(model.IsSentCheck);
                _strBd.Append("','");
                _strBd.Append(model.CheckederRemark);
                _strBd.Append("','");
                _strBd.Append(model.Notes);
                _strBd.Append("','");
                _strBd.Append(model.HolesNum);
                _strBd.Append("','");
                _strBd.Append(model.MachineSampleNum);
                _strBd.Append("','");
                _strBd.Append(model.MachineItemName);
                _strBd.Append("','");
                _strBd.Append(model.SampleSource);
                _strBd.Append("','");
                _strBd.Append(model.Contact);
                _strBd.Append("','");
                _strBd.Append(model.ContactPhone);
                _strBd.Append("','");
                _strBd.Append(model.IsDestruction);
                _strBd.Append("','");
                _strBd.Append(model.DestructionKG);
                _strBd.Append("','");
                _strBd.Append(model.Area);
                _strBd.Append("','");
                _strBd.Append(model.SampleAmount);
                _strBd.Append("')");
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
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
        public int Insert(clsResult model, out string errMsg)
        {
            _strBd.Length=0;
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                _strBd.Append("INSERT INTO tResult");
                _strBd.Append("(SysCode,ResultType,CheckNo,StdCode,SampleCode,CheckedCompany,");
                _strBd.Append("CheckedCompanyName,CheckedComDis,CheckPlaceCode,FoodCode,");
                if (model.ProduceDate != null)
                    _strBd.Append("ProduceDate,");
                _strBd.Append("ProduceCompany,ProducePlace,SentCompany,Provider,TakeDate,CheckStartDate,CheckEndDate,");
                _strBd.Append("ImportNum,SaveNum,Unit,SampleBaseNum,SampleNum,SampleUnit,SampleLevel,SampleModel,SampleState,");
                _strBd.Append("Standard,CheckMachine,CheckTotalItem,CheckValueInfo,StandValue,Result,ResultInfo,UpperCompany,UpperCompanyName,");
                _strBd.Append("OrCheckNo,CheckType,CheckUnitCode,Checker,Assessor,Organizer,Remark,CheckPlanCode,SaleNum,Price,CheckederVal,");
                _strBd.Append("IsSentCheck,CheckederRemark,Notes, HolesNum,MachineSampleNum,MachineItemName,fTpye,testPro,quanOrQual,dataNum,checkedUnit)");
                _strBd.Append("VALUES('");
                _strBd.Append(model.SysCode);
                _strBd.Append("','");
                _strBd.Append(model.ResultType);
                _strBd.Append("','");
                _strBd.Append(model.CheckNo);
                _strBd.Append("','");
                _strBd.Append(model.StdCode);
                _strBd.Append("','");
                _strBd.Append(model.SampleCode);
                _strBd.Append("','");
                _strBd.Append(model.CheckedCompany);
                _strBd.Append("','");
                _strBd.Append(model.CheckedCompanyName);
                _strBd.Append("','");
                _strBd.Append(model.CheckedComDis);
                _strBd.Append("','");
                _strBd.Append(model.CheckPlaceCode);
                _strBd.Append("','");
                _strBd.Append(model.FoodCode);
                _strBd.Append("','");
                if (model.ProduceDate != null)
                {
                    _strBd.Append(model.ProduceDate);
                    _strBd.Append("','");
                }
                _strBd.Append(model.ProduceCompany);
                _strBd.Append("','");
                _strBd.Append(model.ProducePlace);
                _strBd.Append("','");
                _strBd.Append(model.SentCompany);
                _strBd.Append("','");
                _strBd.Append(model.Provider);
                _strBd.Append("','");
                _strBd.Append(model.TakeDate);
                _strBd.Append("','");
                _strBd.Append(model.CheckStartDate);
                _strBd.Append("','");
                _strBd.Append(model.CheckEndDate);
                _strBd.Append("','");
                _strBd.Append(model.ImportNum);
                _strBd.Append("','");
                _strBd.Append(model.SaveNum);
                _strBd.Append("','");
                _strBd.Append(model.Unit);
                _strBd.Append("',");
                _strBd.Append(model.SampleBaseNum);
                _strBd.Append(",");
                _strBd.Append(model.SampleNum);
                _strBd.Append(",'");
                _strBd.Append(model.SampleUnit);
                _strBd.Append("','");
                _strBd.Append(model.SampleLevel);
                _strBd.Append("','");
                _strBd.Append(model.SampleModel);
                _strBd.Append("','");
                _strBd.Append(model.SampleState);
                _strBd.Append("','");
                _strBd.Append(model.Standard);
                _strBd.Append("','");
                _strBd.Append(model.CheckMachine);
                _strBd.Append("','");
                _strBd.Append(model.CheckTotalItem);
                _strBd.Append("','");
                _strBd.Append(model.CheckValueInfo);
                _strBd.Append("','");
                _strBd.Append(model.StandValue);
                _strBd.Append("','");
                _strBd.Append(model.Result);
                _strBd.Append("','");
                _strBd.Append(model.ResultInfo);
                _strBd.Append("','");
                _strBd.Append(model.UpperCompany);
                _strBd.Append("','");
                _strBd.Append(model.UpperCompanyName);
                _strBd.Append("','");
                _strBd.Append(model.OrCheckNo);
                _strBd.Append("','");
                _strBd.Append(model.CheckType);
                _strBd.Append("','");
                _strBd.Append(model.CheckUnitCode);
                _strBd.Append("','");
                _strBd.Append(model.Checker);
                _strBd.Append("','");
                _strBd.Append(model.Assessor);
                _strBd.Append("','");
                _strBd.Append(model.Organizer);
                _strBd.Append("','");
                _strBd.Append(model.Remark);
                _strBd.Append("','");
                _strBd.Append(model.CheckPlanCode);
                _strBd.Append("',");
                _strBd.Append(model.SaleNum);
                _strBd.Append(",");
                _strBd.Append(model.Price);
                _strBd.Append(",'");
                _strBd.Append(model.CheckederVal);
                _strBd.Append("','");
                _strBd.Append(model.IsSentCheck);
                _strBd.Append("','");
                _strBd.Append(model.CheckederRemark);
                _strBd.Append("','");
                _strBd.Append(model.Notes);
                _strBd.Append("','");
                _strBd.Append(model.HolesNum);
                _strBd.Append("','");
                _strBd.Append(model.MachineSampleNum);
                _strBd.Append("','");
                _strBd.Append(model.MachineItemName);
                _strBd.Append("','");

                //安徽项目
                _strBd.Append(model.fTpye);
                _strBd.Append("','");
                _strBd.Append(model.testPro);
                _strBd.Append("','");
                _strBd.Append(model.quanOrQual);
                _strBd.Append("','");
                _strBd.Append(model.dataNum);
                _strBd.Append("','");
                _strBd.Append(model.checkedUnit);
                _strBd.Append("')");
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
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
                string sql = string.Format("SELECT syscode FROM tResult WHERE syscode LIKE '{0}' ORDER BY SYSCODE DESC", prevCode + StringUtil.RepeatChar('_', lev));
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
                _strBd.Length=0;
                _strBd.Append("UPDATE tResult SET ");
                _strBd.Append("IsSended=");
                _strBd.Append(model.IsSended);
                _strBd.Append(",IsReSended=");
                _strBd.Append(model.IsReSended);
                _strBd.Append(",SendedDate='");
                _strBd.Append(model.SendedDate);
                _strBd.Append("',Sender='");
                _strBd.Append(model.Sender);
                _strBd.Append("'");
                _strBd.Append(" WHERE SysCode='");
                _strBd.Append(model.SysCode);
                _strBd.Append("'");
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                _strBd.Length = 0;

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
                _strBd.Length=0;

                _strBd.Append("SELECT A.CheckedCompany & Year(Date()) & A.CheckNo AS CheckSheetNO,");

                _strBd.Append("(SELECT Name FROM tFoodClass WHERE A.FoodCode=tFoodClass.SysCode) AS FoodName,");
                _strBd.Append("A.SampleModel,A.SampleState,A.ProduceInfo,A.SampleNum,A.TakeDate,B.Address,");
                _strBd.Append("(SELECT Name FROM tUserInfo WHERE tUserInfo.UserCode=A.Checker) AS Checker,");
                _strBd.Append("(SELECT FullName FROM tCompany WHERE A.CheckedCompany=tCompany.SysCode) AS ComName,");
                _strBd.Append("(SELECT ItemDes FROM tCheckItem WHERE A.CheckTotalItem=tCheckItem.SysCode) AS CheckItem,");
                _strBd.Append("(SELECT StandardValue FROM tCheckItem WHERE A.CheckTotalItem=tCheckItem.SysCode) AS StandardValue,");
                _strBd.Append("A.CheckValueInfo,A.ResultInfo,A.CheckStartDate,A.Result,A.Remark");
                _strBd.Append(" FROM tResult AS A LEFT JOIN tCompany AS B on A.CheckedCompany=B.SysCode");

                if (!whereSql.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(whereSql);
                }
                if (!orderBySql.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderBySql);
                }
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "Result" };

                dtbl = DataBase.GetDataSet(cmd, names, out sErrMsg).Tables["Result"];
                _strBd.Length = 0;
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
            _strBd.Length = 0;
			try
			{
				_strBd.Append("SELECT");
				_strBd.Append(" (select ShortName from tUserUnit where sysCode='" + ShareOption.DefaultUserUnitCode + "') As ShortName,");
				_strBd.Append("A.CheckNo,B.FullName As ComName,B.Address,(select Name from tCompanyKind where tCompanyKind.sysCode=B.KindCode) As CompanyKind,");
				_strBd.Append("B.CompanyID,B.PostCode,B.LinkMan,B.LinkInfo,B.OtherCodeInfo,(select Name from tFoodClass where A.FoodCode=tFoodClass.SysCode) As FoodName,");
				_strBd.Append("A.CheckType,A.SampleModel,A.SampleLevel,");
				_strBd.Append("A.SampleState,A.Provider,A.SampleNum,A.SampleBaseNum,A.ImportNum,A.SentCompany,A.SaveNum,A.Remark,A.TakeDate,");
				_strBd.Append("(select Name from tUserInfo where tUserInfo.UserCode=A.Checker) as Checker,");
				_strBd.Append("(select ItemDes from tCheckItem where A.CheckTotalItem=tCheckItem.SysCode) as CheckItem,");
				_strBd.Append("(select StdDes from tStandard where A.Standard=tStandard.SysCode) As ReferStandard,");
				_strBd.Append("A.StandValue as StandardValue,");
				_strBd.Append("A.CheckValueInfo,A.ResultInfo,A.SampleCode,A.Result,A.CheckStartDate,");
				_strBd.Append("(select Name from tUserInfo where tUserInfo.UserCode=A.Assessor) as Assessor");
                _strBd.Append(" from tResult As A left join tCompany As B on A.CheckedCompany=B.SysCode");

				if(!whereSql.Equals(""))
				{
                    _strBd.Append(" WHERE ");
                    _strBd.Append(whereSql);
				}
				if(!orderBySql.Equals(""))
				{
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderBySql);
				}
                string[] cmd = new string[1] { _strBd.ToString() };
				string[] names=new string[1]{"Result"};
				dtbl=DataBase.GetDataSet(cmd,names,out errMsg).Tables["Result"];
                _strBd.Length = 0;
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
            _strBd.Length = 0;
			try
			{
				_strBd.Append("SELECT");
				_strBd.AppendFormat(" (SELECT ShortName from tUserUnit where sysCode='{0}') As ShortName,A.CheckNo,",ShareOption.DefaultUserUnitCode);
				_strBd.Append("(select Name from tFoodClass where A.FoodCode=tFoodClass.SysCode) As FoodName,A.Provider,A.SampleModel,(select StdDes from tStandard where A.Standard=tStandard.SysCode) As ReferStandard,");
				_strBd.Append("A.ProduceDate,A.Price,A.ImportNum+A.Unit As ImportNumUnit,A.SaveNum+A.Unit As SaveNumUnit,(select FullName from tCompany where A.ProduceCompany=tCompany.SysCode And tCompany.Property='生产单位') As ProduceCompanyName,");
				_strBd.Append("(select LinkMan from tCompany where A.ProduceCompany=tCompany.SysCode And tCompany.Property='生产单位') As ProduceLinkMan,B.CompanyID,B.Incorporator,B.LinkInfo,B.PostCode,B.FullName As ComName,B.Address,");
				_strBd.Append("A.SampleState,A.Provider,A.SampleNum,A.SampleBaseNum,A.ImportNum,A.SentCompany,A.SaveNum,A.Remark,A.TakeDate,");
				_strBd.Append("(select Name from tDistrict where B.DistrictCode=tDistrict.SysCode) As DistrictName,(select Name from tCompanyKind where B.KindCode=tCompanyKind.SysCode) As KindName,");
				_strBd.Append("B.ComProperty,(select FullName from tUserUnit where sysCode='0001') As CheckUnitFullName,(select LinkMan from tUserUnit where sysCode='0001') As CheckUnitLinkMan,");
				_strBd.Append("(select ItemDes from tCheckItem where A.CheckTotalItem=tCheckItem.SysCode) as CheckItem,(select MachineName from tMachine where A.CheckMachine=tMachine.SysCode) as MachineName,A.TakeDate,str(A.SampleNum)+A.SampleUnit As SampleNumUnit,");
				_strBd.Append("str(A.SampleBaseNum)+A.SampleUnit As SampleBaseNumUnit,A.StandValue+A.ResultInfo As StandValueInfo,A.CheckValueInfo+A.ResultInfo As CheckValueInfo,A.Result,A.CheckederVal,A.IsSentCheck,A.CheckederRemark,A.Remark ");
				_strBd.Append(" from tResult As A left join tCompany As B on A.CheckedCompany=B.SysCode");

				if(!whereSql.Equals(""))
				{
                    _strBd.Append(" WHERE ");
                    _strBd.Append(whereSql);
				}
				if(!orderBySql.Equals(""))
				{
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderBySql);
				}
			   string[] cmd = new string[1] { _strBd.ToString() };
				string[] names=new string[1]{"Result"};
				dt=DataBase.GetDataSet(cmd,names,out errMsg).Tables["Result"];
                _strBd.Length=0;
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
                _strBd.Length=0;
                _strBd.Append("SELECT COUNT(*) FROM TRESULT");
                if (!strWhere.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(strWhere);
                }
                Object obj = DataBase.GetOneValue(_strBd.ToString(), out errMsg);
                _strBd.Length = 0;
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
                        if (comstdcode.Length == ShareOption.CompanyStdCodeLevel)
                        {
                            upperCompanyName = com_FullName;
                            upperCompany = dt.Rows[i]["CheckedCompany"].ToString();
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
