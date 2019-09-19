using System;
using System.Data;

namespace DY.FoodClientLib
{
	/// <summary>
	/// clsFss_StandardOpr 的摘要说明。
	/// </summary>
	public class clsFss_StandardOpr
	{
		private string Conncentstr;
		public clsFss_StandardOpr(string Connstr)
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
				string selectSql="select SysCode,StdCode,StdDes,ShortCut,StdInfo,StdType,IsLock,IsReadOnly,Remark"
						+ " from tFss_Standard";
					
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
				sNames[0]="Fss_Standard";
				dt=DataBase.GetDataSet(Conncentstr,sCmd,sNames,out sErrMsg).Tables["Fss_Standard"];
			}
			catch(Exception e)
			{
				//				Log.WriteLog("查询clsFss_Standard",e);
				sErrMsg=e.Message;
			}

			return dt;
		}
	}
}
