using System;
using System.Data;

namespace DY.FoodClientLib
{
	/// <summary>
	/// clsFss_CheckComTypeOpr 的摘要说明。
	/// </summary>
	public class clsFss_CheckComTypeOpr
	{
		private string Conncentstr;
		public clsFss_CheckComTypeOpr(string Connstr)
		{
			Conncentstr=Connstr;
		}

				
		/// <summary>
		/// 根据查询串条件查询记录
		/// </summary>
		/// <param name="whereSql">查询条件串,不含Where</param>
		/// <param name="orderBySql">排序串,不含Order</param>
		/// <returns></returns>
		public DataTable GetAsDataTable(string whereSql, string orderBySql,int type)
		{
			string sErrMsg=string.Empty; 
			DataTable dt=null;
			
			try
			{
				string selectSql="";
				if(type==0)
				{
					selectSql="select ID,TypeName,NameCall,AreaCall,VerType,IsLock,IsReadOnly,ComKind "
						+ " from tFss_checkcomtype";			
				}
				else if(type==1)
				{
					selectSql="select TypeName from tFss_checkcomtype";
				}
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
				sNames[0]="Fss_CheckComType";
				dt=DataBase.GetDataSet(Conncentstr,sCmd,sNames,out sErrMsg).Tables["Fss_CheckComType"];
			}
			catch(Exception e)
			{
				sErrMsg=e.Message;
			}

			return dt;
		}

		public static string ValueFromName(string Connstr,string sfile,string sname)
		{
			string sErrMsg=string.Empty; 
			if(sname.Equals("")||sfile.Equals(""))
			{
				return "";
			}

			try
			{
				string sql="select " + sfile +" from tFss_CheckComType where TypeName='" + sname 
					+ "' ";
				Object o=DataBase.GetOneValue(Connstr,sql,out sErrMsg);
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
				return null;
			}
		}

	}
}
