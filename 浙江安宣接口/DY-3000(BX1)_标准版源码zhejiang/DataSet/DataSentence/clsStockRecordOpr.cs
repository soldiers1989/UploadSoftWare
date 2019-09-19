using System;
using System.Data;

namespace DYSeriesDataSet
{
	/// <summary>
	/// clsStockRecordOpr 的摘要说明。
	/// </summary>
	public class clsStockRecordOpr
	{
		public clsStockRecordOpr()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 部分修改保存
		/// </summary>
		/// <param name="OprObject">对象clsStockRecord的一个实例参数</param>
		/// <returns></returns>
		public int UpdatePart(clsStockRecord OprObject,string sOldCode,out string sErrMsg)
		{
			sErrMsg=string.Empty; 
			int rtn=0;			

			try
			{
				string updateSql="update tStockRecord set "
					+ "StdCode='" + OprObject.StdCode + "'," 
					+ "CompanyID='" + OprObject.CompanyID + "'," 
					+ "CompanyName='" + OprObject.CompanyName + "'," 
					+ "DisplayName='" + OprObject.DisplayName + "',"  
					+ "InputDate='" + OprObject.InputDate  + "'," 
					+ "FoodID='" + OprObject.FoodID + "',"
					+ "FoodName='" + OprObject.FoodName + "',"
					+ "Model='" + OprObject.Model + "'," 
					+ "InputNumber=" + OprObject.InputNumber  + "," 
					+ "OutputNumber=" + OprObject.OutputNumber  + ","
					+ "Unit='" + OprObject.Unit + "'," 
					+ "ProduceDate='" + OprObject.ProduceDate + "'," 
					+ "ExpirationDate='" + OprObject.ExpirationDate + "',"
					+ "ProduceCompanyID='" + OprObject.ProduceCompanyID + "'," 
					+ "ProduceCompanyName='" + OprObject.ProduceCompanyName + "'," 
					+ "PrivoderID='" + OprObject.PrivoderID + "'," 
					+ "PrivoderName='" + OprObject.PrivoderName + "'," 
					+ "LinkInfo='" + OprObject.LinkInfo + "'," 
					+ "LinkMan='" + OprObject.LinkMan + "',"
					+ "CertificateType1='" + OprObject.CertificateType1 + "'," 
					+ "CertificateType2='" + OprObject.CertificateType2 + "'," 
					+ "CertificateType3='" + OprObject.CertificateType3 + "'," 
					+ "CertificateType4='" + OprObject.CertificateType4 + "'," 
					+ "CertificateType5='" + OprObject.CertificateType5 + "'," 
					+ "CertificateType6='" + OprObject.CertificateType6 + "'," 
					+ "CertificateType7='" + OprObject.CertificateType7 + "'," 
					+ "CertificateType8='" + OprObject.CertificateType8 + "'," 
					+ "CertificateType9='" + OprObject.CertificateType9 + "'," 
					+ "CertificateInfo='" + OprObject.CertificateInfo + "'," 
					+ "MakeMan='" + OprObject.MakeMan + "',"
					+ "Remark='" + OprObject.Remark + "',"
					+ "DistrictCode='" + OprObject.DistrictCode + "' "
					+ " where SysCode='" + sOldCode + "' ";
				DataBase.ExecuteCommand(updateSql,out sErrMsg);

				rtn=1;
			}
			catch(Exception e)
			{
				//				Log.WriteLog("更新tStockRecord",e);
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
				string deleteSql="delete from tStockRecord";
	
				if(!whereSql.Equals(""))
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
		/// 根据主键编号删除记录
		/// </summary>
		/// <param name="%pkname%">主键编号</param>
		/// <returns></returns>
		public int DeleteByPrimaryKey(string mainkey,out string sErrMsg)
		{
			sErrMsg=string.Empty;        
			int rtn = 0;

			try
			{
				string deleteSql="delete from tStockRecord"
					+ " where SysCode='" + mainkey + "' ";
				DataBase.ExecuteCommand(deleteSql,out sErrMsg);

				rtn=1;
			}
			catch(Exception e)
			{
				//				Log.WriteLog("通过主键删除tStockRecord",e);
				sErrMsg=e.Message;;
			}

			return rtn;
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
				selectSql="SELECT SysCode,StdCode,CompanyID,CompanyName,DisplayName,InputDate,FoodID,FoodName,Model,InputNumber,OutputNumber,Unit,ProduceDate,ExpirationDate,ProduceCompanyID,ProduceCompanyName,PrivoderID,PrivoderName,LinkInfo,LinkMan,CertificateType1,CertificateType2,CertificateType3,CertificateType4,CertificateType5,CertificateType6,CertificateType7,CertificateType8,CertificateType9,CertificateInfo,MakeMan,Remark,DistrictCode,IsSended,SentDate FROM tStockRecord ";

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
				sNames[0]="StockRecord";
				dt=DataBase.GetDataSet(sCmd,sNames,out sErrMsg).Tables["StockRecord"];
			}
			catch(Exception e)
			{
				//				Log.WriteLog("查询tStockRecord",e);
				sErrMsg=e.Message;
			}

			return dt;
		}

		/// <summary>
		/// 插入一条明细记录
		/// </summary>
		/// <param name="OprObject"></param>
		/// <returns></returns>
		public int Insert(clsStockRecord OprObject,out string sErrMsg)
		{  
			sErrMsg=string.Empty; 
			int rtn = 0;			

			try
			{
				string insertSql="insert into tStockRecord(SysCode,StdCode,CompanyID,CompanyName,DisplayName,InputDate,FoodID,FoodName,Model,InputNumber,OutputNumber,Unit,ProduceDate,ExpirationDate,ProduceCompanyID,ProduceCompanyName,PrivoderID,PrivoderName,LinkInfo,LinkMan,CertificateType1,CertificateType2,CertificateType3,CertificateType4,CertificateType5,CertificateType6,CertificateType7,CertificateType8,CertificateType9,CertificateInfo,MakeMan,Remark,DistrictCode)"
					+ " values('"
					+ OprObject.SysCode + "','" 
					+ OprObject.StdCode + "','"
					+ OprObject.CompanyID + "','" 
					+ OprObject.CompanyName + "','"
					+ OprObject.DisplayName + "','"
					+ OprObject.InputDate  +"','"
					+ OprObject.FoodID  +"','"
					+ OprObject.FoodName  + "','"
					+ OprObject.Model  + "',"
					+ OprObject.InputNumber  + ","
					+ OprObject.OutputNumber  + ",'"
					+ OprObject.Unit  + "','"
					+ OprObject.ProduceDate  + "','"
					+ OprObject.ExpirationDate + "','"
					+ OprObject.ProduceCompanyID  + "','"
					+ OprObject.ProduceCompanyName  + "','"
					+ OprObject.PrivoderID  + "','"
					+ OprObject.PrivoderName  + "','"
					+ OprObject.LinkInfo  + "','"
					+ OprObject.LinkMan  + "','"
					+ OprObject.CertificateType1  + "','"
					+ OprObject.CertificateType2  + "','"
					+ OprObject.CertificateType3  + "','"
					+ OprObject.CertificateType4  + "','"
					+ OprObject.CertificateType5  + "','"
					+ OprObject.CertificateType6  + "','"
					+ OprObject.CertificateType7  + "','"
					+ OprObject.CertificateType8  + "','"
					+ OprObject.CertificateType9  + "','"
					+ OprObject.CertificateInfo  + "','"
					+ OprObject.MakeMan  + "','"
					+ OprObject.Remark + "','"
					+ OprObject.DistrictCode + "')";
				DataBase.ExecuteCommand(insertSql,out sErrMsg);

				rtn = 1;
			}
			catch(Exception e)
			{
				//				Log.WriteLog("添加tSaleRecord",e);
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
				string sql="select syscode from tStockRecord where syscode like '" + prevcode 
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
				string sql="select Stdcode from tStockRecord where Stdcode like '" + prevcode 
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
				string sql="select StdCode from tStockRecord where StdCode='" + code 
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
				string sql="select Count(*) from tStockRecord";
				if(!whereSql.Equals(""))
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
				string updateSql="update tStockRecord set IsSended = true,SentDate='" + System.DateTime.Today.ToString() + "' Where SysCode='" +sOldCode + "'";
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
