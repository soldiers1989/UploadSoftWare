using System;
using System.Data;

namespace DY.FoodClientLib
{
	/// <summary>
	/// clsFss_StandardTypeOpr ��ժҪ˵����
	/// </summary>
	public class clsFss_StandardTypeOpr
	{
		private string Conncentstr;
		public clsFss_StandardTypeOpr(string Connstr)
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
				string selectSql="select StdName,IsLock,IsReadOnly,Remark"
						+ " from tFss_StandardType";

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
				sNames[0]="Fss_StandardType";
				dt=DataBase.GetDataSet(Conncentstr,sCmd,sNames,out sErrMsg).Tables["Fss_StandardType"];
			}
			catch(Exception e)
			{
				//				Log.WriteLog("��ѯclsFss_StandardType",e);
				sErrMsg=e.Message;
			}

			return dt;
		}	
	}
}
