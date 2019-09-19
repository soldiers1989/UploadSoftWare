using System;
using System.Data;

namespace DYSeriesDataSet
{
	/// <summary>
	/// clsCom_UserUnitOpr 的摘要说明。
	/// </summary>
	public class clsCom_UserUnitOpr
	{
		private string Conncentstr;
		public clsCom_UserUnitOpr(string Connstr)
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
				string selectSql="select A.SysCode,A.StdCode,A.FullName,A.ShortName,A.DisplayName,A.ShortCut,A.DistrictCode,B.Name As DistrictName,A.PostCode,A.Address,A.LinkMan,A.LinkInfo,A.WWWInfo,A.IsReadOnly,A.IsLock,A.Remark"
						+ " from tCom_UserUnit As A";
				
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
				sNames[0]="Com_UserUnit";
				dt=DataBase.GetDataSet(Conncentstr,sCmd,sNames,out sErrMsg).Tables["Com_UserUnit"];
			}
			catch(Exception e)
			{
				//				Log.WriteLog("查询clsCom_UserUnit",e);
				sErrMsg=e.Message;
			}

			return dt;
		}	

		public string GetStdCodeServer(string stdcode,out string sFullName)
		{
			string sErrMsg=string.Empty; 
			DataTable dt=null;
			sFullName="";
			if(stdcode.Equals(""))
			{
				return "";
			}

			try
			{
				string sql="select stdcode,FullName from tCom_UserUnit where stdcode='" + stdcode + "'";

				string[] sCmd=new string[1];
				sCmd[0]=sql;
				string[] sNames=new string[1];
				sNames[0]="Com_UserUnit";
				dt=DataBase.GetDataSet(Conncentstr,sCmd,sNames,out sErrMsg).Tables["Com_UserUnit"];
				sFullName=dt.Rows[0]["FullName"].ToString();
				return dt.Rows[0]["stdcode"].ToString();
			}
			catch(Exception e)
			{
				sErrMsg=e.Message;
				return "";
			}		
		}

		public string GetDistrictCode(string stdcode)
		{
			string sErrMsg=string.Empty; 
			if(stdcode.Equals(""))
			{
				return "";
			}

			try
			{
				string sql="select DistrictCode from tCom_UserUnit where stdcode='" + stdcode + "'";
				Object o=DataBase.GetOneValue(Conncentstr,sql,out sErrMsg);
				if(o==null)
				{
					return "";
				}
				else
				{
					return o.ToString();
				}
			}
			catch(Exception e)
			{
				sErrMsg=e.Message;
				return "";
			}		
		}	
	
		public static bool UnitIsExist(string swhere,string Conncentstr)
		{
			string sErrMsg=string.Empty; 
			if(swhere.Equals(""))
			{
				return false;
			}

			try
			{
				string sql="select syscode from tCom_UserUnit where " + swhere;
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
