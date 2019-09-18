using System;

namespace DYSeriesDataSet
{
	/// <summary>
	/// clsUpdate_ResultOpr 的摘要说明。
	/// </summary>
	public class clsUpdate_ResultOpr
	{
		private string Conncentstr;

		public clsUpdate_ResultOpr(string Connstr)
		{
			Conncentstr=Connstr;
		}

		/// <summary>
		/// 插入一条明细记录
		/// </summary>
		/// <param name="OprObject"></param>
		/// <returns></returns>
		public int Insert(clsUpdate_Result OprObject,out string sErrMsg)
		{  
			sErrMsg=string.Empty; 
			int rtn = 0;			

			try
			{
				string insertSql="insert into tFss_Result(SysCode,ResultType,CheckNo,SampleCode,StdCode,CheckedCompany,CheckedCompanyInfo,CheckedComDis,CheckPlace,CheckPlaceInfo,FoodName,FoodInfo,SentCompany,Provider,ProduceDate,ProduceCompany,ProducePlace,ProducePlaceInfo,TakeDate,CheckStartDate,CheckEndDate,ImportNum,SaveNum,Unit,SampleBaseNum,SampleNum,SampleUnit,SampleModel,SampleLevel,SampleState,Standard,CheckMachine,CheckMachineModel,MachineCompany,CheckTotalItem,CheckValueInfo,StandValue,Result,ResultInfo,OrCheckNo,UpperCompany,CheckType,CheckUnitName,CheckUnitInfo,Checker,Assessor,Organizer,UpLoadDate,UpLoader,Remark)"
					+ " values('"
					+ OprObject.SysCode + "','" 
					+ OprObject.ResultType + "','"
					+ OprObject.CheckNo + "','"
					+ OprObject.SampleCode + "','" 
					+ OprObject.StdCode + "','" 
					+ OprObject.CheckedCompany +"','"
					+ OprObject.CheckedCompanyInfo + "','"
					+ OprObject.CheckedComDis + "','"
					+ OprObject.CheckPlace +"','"
					+ OprObject.CheckPlaceInfo +"','"
					+ OprObject.FoodName + "','"
					+ OprObject.FoodInfo +"','"
					+ OprObject.SentCompany + "','"
					+ OprObject.Provider +"','"
					+ OprObject.ProduceDate + "','"
					+ OprObject.ProduceCompany +"','"
					+ OprObject.ProducePlace + "','"
					+ OprObject.ProducePlaceInfo + "','"
					+ OprObject.TakeDate + "','"
					+ OprObject.CheckStartDate + "','"
					+ OprObject.CheckEndDate + "','"
					+ OprObject.ImportNum + "','"
					+ OprObject.SaveNum + "','"
					+ OprObject.Unit + "',"
					+ OprObject.SampleBaseNum + ","
					+ OprObject.SampleNum + ",'"
					+ OprObject.SampleUnit + "','"
					+ OprObject.SampleModel + "','"
					+ OprObject.SampleLevel + "','"
					+ OprObject.SampleState + "','"
					+ OprObject.Standard + "','"
					+ OprObject.CheckMachine + "','"
					+ OprObject.CheckMachineModel + "','"
					+ OprObject.MachineCompany + "','"
					+ OprObject.CheckTotalItem+ "','"
					+ OprObject.CheckValueInfo + "','"
					+ OprObject.StandValue + "','"
					+ OprObject.Result + "','"
					+ OprObject.ResultInfo + "','"
					+ OprObject.OrCheckNo + "','"
					+ OprObject.UpperCompany + "','"
					+ OprObject.CheckType + "','"
					+ OprObject.CheckUnitName + "','"
					+ OprObject.CheckUnitInfo + "','"
					+ OprObject.Checker + "','"
					+ OprObject.Assessor + "','"
					+ OprObject.Organizer + "',"
					+ "GetDate(),'"
					+ OprObject.UpLoader + "','"
					+ OprObject.Remark + "')";
				DataBase.ExecuteCommand(Conncentstr,insertSql,out sErrMsg);

				rtn = 1;
			}
			catch(Exception e)
			{
				//				Log.WriteLog("添加clsSend_Result",e);
				sErrMsg=e.Message;
			}

			return rtn;
		}	

		public int Delete(string whereSql,out string sErrMsg)
		{
			sErrMsg=string.Empty;        
			int rtn = 0;

			try
			{
				string deleteSql="delete from tFss_Result";
				if(!whereSql.Equals(string.Empty))
				{
					deleteSql+= " where " + whereSql;
				}
				DataBase.ExecuteCommand(Conncentstr,deleteSql,out sErrMsg);

				rtn=1;
			}
			catch(Exception e)
			{
				sErrMsg=e.Message;;
			}

			return rtn;
		}
	}
}
