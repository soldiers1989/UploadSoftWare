using System;
using System.Data;

namespace DYSeriesDataSet
{
	/// <summary>
	/// clsCom_ProduceAreaOpr ��ժҪ˵����
	/// </summary>
	public class clsCom_ProduceAreaOpr
	{
		private string Conncentstr;
		public clsCom_ProduceAreaOpr(string Connstr)
		{
			Conncentstr=Connstr;
		}
		
		/// <summary>
		/// ���ݲ�ѯ��������ѯ��¼
		/// </summary>
		/// <param name="whereSql">��ѯ������,����Where</param>
		/// <param name="orderBySql">����,����Order</param>
		/// <returns></returns>
		public DataTable GetAsDataTable(string whereSql, string orderBySql)
		{
			string sErrMsg=string.Empty; 
			DataTable dt=null;
			
			try
			{
				string selectSql="select SysCode,StdCode,Name,ShortCut,DistrictIndex,CheckLevel,IsLocal,IsLock,IsReadOnly,Remark from tCom_ProduceArea";

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
				sNames[0]="Com_ProduceArea";
				dt=DataBase.GetDataSet(Conncentstr,sCmd,sNames,out sErrMsg).Tables["Com_ProduceArea"];
			}
			catch(Exception e)
			{
				//				Log.WriteLog("��ѯclsCom_ProduceArea",e);
				sErrMsg=e.Message;
			}

			return dt;
		}	
	}
}
