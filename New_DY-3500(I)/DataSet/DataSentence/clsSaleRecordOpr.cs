using System;
using System.Data;

namespace DataSetModel
{
	/// <summary>
	/// clsSaleRecordOpr ��ժҪ˵����
	/// </summary>
	public class clsSaleRecordOpr
	{
		public clsSaleRecordOpr()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// �����޸ı���
		/// </summary>
		/// <param name="OprObject">����clsSaleRecord��һ��ʵ������</param>
		/// <returns></returns>
		public int UpdatePart(clsSaleRecord OprObject,string sOldCode,out string sErrMsg)
		{
			sErrMsg=string.Empty; 
			int rtn=0;			

			try
			{
				string updateSql="update tSaleRecord set "
					+ "StdCode='" + OprObject.StdCode + "'," 
					+ "CompanyID='" + OprObject.CompanyID + "'," 
					+ "CompanyName='" + OprObject.CompanyName + "'," 
					+ "DisplayName='" + OprObject.DisplayName + "'," 
					+ "SaleDate='" + OprObject.SaleDate + "'," 
					+ "FoodID='" + OprObject.FoodID + "',"
					+ "FoodName='" + OprObject.FoodName + "',"
					+ "Model='" + OprObject.Model + "'," 
					+ "SaleNumber=" + OprObject.SaleNumber + "," 
					+ "Price=" + OprObject.Price + ","
					+ "Unit='" + OprObject.Unit + "'," 
					+ "PurchaserID='" + OprObject.PurchaserID + "'," 
					+ "PurchaserName='" + OprObject.PurchaserName + "'," 
					+ "LinkInfo='" + OprObject.LinkInfo + "'," 
					+ "LinkMan='" + OprObject.LinkMan + "'," 
					+ "MakeMan='" + OprObject.MakeMan + "',"
					+ "Remark='" + OprObject.Remark + "', "
					+ "DistrictCode='" + OprObject.DistrictCode + "' "
					+ " where SysCode='" + sOldCode + "' ";
				DataBase.ExecuteCommand(updateSql,out sErrMsg);

				rtn=1;
			}
			catch(Exception e)
			{
				//				Log.WriteLog("����tSaleRecord",e);
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
				string deleteSql="delete from tSaleRecord";
	
				if(!whereSql.Equals(string.Empty))
				{
					deleteSql+= " where " + whereSql;
				}
				DataBase.ExecuteCommand(deleteSql,out sErrMsg);

				rtn=1;
			}
			catch(Exception e)
			{
				sErrMsg=e.Message;
			}

			return rtn;
		}
		
		/// <summary>
		/// �����������ɾ����¼
		/// </summary>
		/// <param name="%pkname%">�������</param>
		/// <returns></returns>
		public int DeleteByPrimaryKey(string mainkey,out string sErrMsg)
		{
			sErrMsg=string.Empty;        
			int rtn = 0;

			try
			{
				string deleteSql="delete from tSaleRecord"
					+ " where SysCode='" + mainkey + "' ";
				DataBase.ExecuteCommand(deleteSql,out sErrMsg);

				rtn=1;
			}
			catch(Exception e)
			{
				//				Log.WriteLog("ͨ������ɾ��tSaleRecord",e);
				sErrMsg=e.Message;;
			}

			return rtn;
		}
		
		/// <summary>
		/// ���ݲ�ѯ��������ѯ��¼
		/// </summary>
		/// <param name="whereSql">��ѯ������,����Where</param>
		/// <param name="orderBySql">����,����Order</param>
		/// <returns></returns>
		public DataTable GetAsDataTable(string whereSql, string orderBySql,int type)
		{
			string sErrMsg=string.Empty; 
			DataTable dt=null;
			
			try
			{
				string selectSql=string.Empty;
				selectSql="SELECT tSaleRecord.SysCode, tSaleRecord.StdCode, tSaleRecord.CompanyID, tSaleRecord.CompanyName,DisplayName, tSaleRecord.SaleDate, tSaleRecord.FoodID, tSaleRecord.FoodName, tSaleRecord.Model, tSaleRecord.SaleNumber, tSaleRecord.Price,Unit, tSaleRecord.PurchaserID, tSaleRecord.PurchaserName, tSaleRecord.LinkInfo, tSaleRecord.LinkMan, tSaleRecord.MakeMan, tSaleRecord.Remark,DistrictCode,IsSended,SentDate FROM tSaleRecord ";

				if(!whereSql.Equals(string.Empty))
				{
					selectSql+= " where " + whereSql;
				}
				if(!orderBySql.Equals(string.Empty))
				{
					selectSql+= " order by " + orderBySql;
				}
				string[] sCmd=new string[1];
				sCmd[0]=selectSql;
				string[] sNames=new string[1];
				sNames[0]="SaleRecord";
				dt=DataBase.GetDataSet(sCmd,sNames,out sErrMsg).Tables["SaleRecord"];
			}
			catch(Exception e)
			{
				//				Log.WriteLog("��ѯclsCompany",e);
				sErrMsg=e.Message;
			}

			return dt;
		}

		/// <summary>
		/// ����һ����ϸ��¼
		/// </summary>
		/// <param name="OprObject"></param>
		/// <returns></returns>
		public int Insert(clsSaleRecord OprObject,out string sErrMsg)
		{  
			sErrMsg=string.Empty; 
			int rtn = 0;			

			try
			{
				string insertSql="insert into tSaleRecord(SysCode,StdCode,CompanyID,CompanyName,DisplayName,SaleDate,FoodID,FoodName,Model,SaleNumber,Price,Unit,PurchaserID,PurchaserName,LinkInfo,LinkMan,MakeMan,Remark,DistrictCode)"
					+ " values('"
					+ OprObject.SysCode + "','" 
					+ OprObject.StdCode + "','"
					+ OprObject.CompanyID + "','" 
					+ OprObject.CompanyName + "','"
					+ OprObject.DisplayName + "','"
					+ OprObject.SaleDate +"','"
					+ OprObject.FoodID  +"','"
					+ OprObject.FoodName  + "','"
					+ OprObject.Model  + "',"
					+ OprObject.SaleNumber  + ","
					+ OprObject.Price  + ",'"
					+ OprObject.Unit   + "','"
					+ OprObject.PurchaserID  + "','"
					+ OprObject.PurchaserName  + "','"
					+ OprObject.LinkInfo  + "','"
					+ OprObject.LinkMan  + "','"
					+ OprObject.MakeMan  + "','"
					+ OprObject.Remark + "','"
					+ OprObject.DistrictCode + "')";
				DataBase.ExecuteCommand(insertSql,out sErrMsg);

				rtn = 1;
			}
			catch(Exception e)
			{
				//				Log.WriteLog("���tSaleRecord",e);
				sErrMsg=e.Message;
			}

			return rtn;
		}

		public int GetMaxNO(string prevcode,int lev,out string sErrMsg)
		{
			sErrMsg=string.Empty; 
			int rtn;

			try
			{
				string sql="select syscode from tSaleRecord where syscode like '" + prevcode 
					+ "' order by syscode desc";
				Object o=DataBase.GetOneValue(sql,out sErrMsg);
				if(o==null)
				{
					rtn=0;
				}
				else
				{
					string rightStr=o.ToString().Substring(o.ToString().Length-lev,lev);
					rtn=Convert.ToInt32(rightStr);
				}
			}
			catch(Exception e)
			{
				sErrMsg=e.Message;
				rtn=-1;
			}

			return rtn;
		}

		public int GetStdCodeMaxNO(string prevcode,int lev,out string sErrMsg)
		{
			sErrMsg=string.Empty; 
			int rtn;

			try
			{
				string sql="select Stdcode from tSaleRecord where Stdcode like '" + prevcode 
					 + "' order by Stdcode desc";
				Object o=DataBase.GetOneValue(sql,out sErrMsg);
				if(o==null)
				{
					rtn=0;
				}
				else
				{
					string rightStr=o.ToString().Substring(o.ToString().Length-lev,lev);
					rtn=Convert.ToInt32(rightStr);
				}
			}
			catch(Exception e)
			{
				sErrMsg=e.Message;
				rtn=-1;
			}

			return rtn;
		}

		public bool ExistSameValue(string code)
		{
			string sErrMsg=string.Empty; 

			try
			{
				string sql="select StdCode from tSaleRecord where StdCode='" + code 
					+ "' ";
				Object o=DataBase.GetOneValue(sql,out sErrMsg);
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
				return true;
			}
		}	

		public int GetRecCount(string whereSql,out string sErrMsg)
		{
			sErrMsg=string.Empty; 
			int rtn;

			try
			{
				string sql="select Count(*) from tSaleRecord";
				if(!whereSql.Equals(string.Empty))
				{
					sql+= " where " + whereSql;
				}
				Object o=DataBase.GetOneValue(sql,out sErrMsg);
				if(o==null)
				{
					rtn=0;
				}
				else
				{
					rtn=Convert.ToInt32(o);
				}
			}
			catch(Exception e)
			{
				sErrMsg=e.Message;
				rtn=-1;
			}

			return rtn;
		}	

		public int UpdateSendFlag(string sOldCode,out string sErrMsg)
		{
			sErrMsg=string.Empty; 
			int rtn=0;			

			try
			{
				string updateSql="update tSaleRecord set IsSended = true,SentDate='" +  System.DateTime.Today.ToString() + "' Where SysCode='" +sOldCode + "'";
				DataBase.ExecuteCommand(updateSql,out sErrMsg);

				rtn=1;
			}
			catch(Exception e)
			{
				sErrMsg=e.Message;
			}

			return rtn;
		}
	}
}
