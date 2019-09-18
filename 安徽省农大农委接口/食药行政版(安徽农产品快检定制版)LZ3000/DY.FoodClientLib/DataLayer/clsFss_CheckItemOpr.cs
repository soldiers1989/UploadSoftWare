using System;
using System.Data;

namespace DY.FoodClientLib
{
	/// <summary>
	/// clsFss_CheckItemOpr ��ժҪ˵����
	/// </summary>
	public class clsFss_CheckItemOpr
	{
		private string Conncentstr;
		public clsFss_CheckItemOpr(string Connstr)
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
				string selectSql="select SysCode,StdCode,ItemDes,CheckType,"
						+ "StandardCode,StandardValue,Unit,DemarcateInfo,"
						+ "TestValue,OperateHelp,CheckLevel,IsReadOnly,IsLock,Remark"
						+ " from tFss_CheckItem";

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
				sNames[0]="Fss_CheckItem";
				dt=DataBase.GetDataSet(Conncentstr,sCmd,sNames,out sErrMsg).Tables["Fss_CheckItem"];
			}
			catch(Exception e)
			{
				//				Log.WriteLog("��ѯclsFss_CheckItem",e);
				sErrMsg=e.Message;
			}

			return dt;
		}
	
	}
}
