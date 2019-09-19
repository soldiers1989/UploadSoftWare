using System;
using System.Data;

namespace DY.FoodClientLib
{
	/// <summary>
	/// clsFss_CompanyOpr 的摘要说明。
	/// </summary>
	public class clsFss_CompanyOpr
	{
		private string Conncentstr;
		public clsFss_CompanyOpr(string Connstr)
		{
			Conncentstr=Connstr;
		}
		
		/// <summary>
		/// 根据查询串条件查询记录
		/// </summary>
		/// <param name="whereSql">查询条件串,不含Where</param>
		/// <param name="orderBySql">排序串,不含Order</param>
		/// <returns></returns>
		public DataTable GetAsDataTable(string whereSql, string orderBySql)
		{
			string sErrMsg=string.Empty; 
			DataTable dt=null;
			
			try
			{
				string selectSql="select A.SysCode,A.StdCode,A.CompanyID,A.OtherCodeInfo,A.FullName,A.ShortName,A.DisplayName,A.ShortCut,A.Property,A.KindCode,A.RegCapital,A.Unit,A.Incorporator,A.RegDate,A.DistrictCode,A.PostCode,A.Address,A.LinkMan,A.LinkInfo,A.CreditLevel,A.CreditRecord,A.ProductInfo,A.OtherInfo,A.CheckLevel,A.FoodSafeRecord,A.IsReadOnly,A.IsLock,A.Remark"
						+ " FROM tFss_Company AS A ";
				
				if(!whereSql.Equals(""))
				{
					selectSql+= " where " + whereSql;
				}
				if(!orderBySql.Equals(""))
				{
					selectSql+= " order by " + orderBySql;
				}
				string[] sCmd=new string[1];
				sCmd[0]=selectSql;
				string[] sNames=new string[1];
				sNames[0]="Fss_Company";
				dt=DataBase.GetDataSet(Conncentstr,sCmd,sNames,out sErrMsg).Tables["Fss_Company"];
			}
			catch(Exception e)
			{
				//				Log.WriteLog("查询clsFss_Company",e);
				sErrMsg=e.Message;
			}

			return dt;
		}
	
		public static bool CompanyIsExist(string swhere,string Conncentstr)
		{
			string sErrMsg=string.Empty; 
			if(swhere.Equals(""))
			{
				return false;
			}

			try
			{
				string sql="select syscode from tFss_Company where " + swhere;
				Object o=DataBase.GetOneValue(Conncentstr,sql,out sErrMsg);
				if(o==null)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			catch(Exception e)
			{
				sErrMsg=e.Message;
				return false;
			}		
		}			
	}
}
