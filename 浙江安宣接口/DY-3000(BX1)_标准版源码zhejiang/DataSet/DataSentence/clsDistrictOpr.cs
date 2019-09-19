using System;
using System.Data;
using System.Text;

namespace DYSeriesDataSet
{
	/// <summary>
	/// 行政机构
	/// </summary>
	public class clsDistrictOpr
	{
		public clsDistrictOpr()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        StringBuilder sb = new StringBuilder();
		/// <summary>
		/// 部分修改保存
		/// </summary>
		/// <param name="model">对象clsDistrict的一个实例参数</param>
		/// <returns></returns>
        public int UpdatePart(clsDistrict model, int lev, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
         
            try
            {
                sb.Length = 0;
                sb.Append("UPDATE tDistrict SET ");
                sb.Append(" StdCode='");
                sb.Append(model.StdCode);
                sb.Append("'");
                sb.Append(",Name='");
                sb.Append(model.Name);
                sb.Append("'");
                sb.Append(",ShortCut='");
                sb.Append(model.ShortCut);
                sb.Append("'");
                sb.Append(",DistrictIndex=");
                sb.Append(model.DistrictIndex);
                sb.Append(",CheckLevel='");
                sb.Append(model.CheckLevel);
                sb.Append("'");
                sb.Append(",IsLocal=");
                sb.Append(model.IsLocal);
                sb.Append(",IsReadOnly=");
                sb.Append(model.IsReadOnly);
                sb.Append(",IsLock=");
                sb.Append(model.IsLock);
                sb.Append(",Remark='");
                sb.Append(model.Remark);
                sb.Append("' WHERE SysCode='");
                sb.Append(model.SysCode);
                sb.Append("'");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;

                //string updateSql="update tDistrict set "
                //+ "StdCode='" + model.StdCode + "'," 
                //+ "Name='" + model.Name + "'," 
                //+ "ShortCut='" + model.ShortCut + "'," 
                //+ "DistrictIndex=" + model.DistrictIndex + "," 
                //+ "CheckLevel='" + model.CheckLevel + "',"
                //+ "IsLocal=" + model.IsLocal + ","
                //+ "IsReadOnly=" + model.IsReadOnly + "," 
                //+ "IsLock=" + model.IsLock + "," 
                //+ "Remark='" + model.Remark + "'" 
                //+ " where SysCode='" + model.SysCode + "' ";
                //DataBase.ExecuteCommand(updateSql,out sErrMsg);

                if (model.IsLocal)
                {
                    sb.Append("UPDATE tDistrict SET ");
                    sb.Append(" IsLocal=");
                    sb.Append(model.IsLocal);
                    sb.Append(" WHERE SysCode LIKE '");
                    sb.Append(model.SysCode);
                    sb.Append("%'");
                    DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                    sb.Length = 0;
                    //string sql="update tDistrict set "
                    //    + "IsLocal=" + model.IsLocal 
                    //    + " where SysCode like '" + model.SysCode + "%'";
                    //DataBase.ExecuteCommand(sql,out sErrMsg);
                }

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("更新clsDistrict",e);
                errMsg = e.Message;
            }

            return rtn;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
		public int Delete(string whereSql,out string errMsg)
		{
			errMsg=string.Empty;        
			int rtn = 0;

			try
			{		
	            sb.Length=0;
				sb.Append("DELETE FROM TDISTRICT");

				if(!whereSql.Equals(""))
				{
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
				}
				DataBase.ExecuteCommand(sb.ToString(),out errMsg);

				rtn=1;
			}
			catch(Exception e)
			{
				errMsg=e.Message;
			}

			return rtn;
		}
		
		/// <summary>
		/// 根据主键编号删除记录
		/// </summary>
		/// <param name="%pkname%">主键编号</param>
		/// <returns></returns>
		public int DeleteByPrimaryKey(string mainkey,out string errMsg)
		{
			errMsg=string.Empty;        
			int rtn = 0;

			try
			{
				string deleteSql="DELETE FROM TDISTRICT WHERE SYSCODE='" + mainkey + "' ";
				DataBase.ExecuteCommand(deleteSql,out errMsg);
				rtn=1;
			}
			catch(Exception e)
			{
				//Log.WriteLog("通过主键删除clsDistrict",e);
				errMsg=e.Message;;
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
			string errMsg=string.Empty; 
			DataTable dt=null;

            try
            {
                sb.Length = 0;
                if (type == 0)
                {
                    sb.Append("SELECT SysCode,StdCode,Name,ShortCut,DistrictIndex,CheckLevel,");
                    sb.Append("IsLocal,IsLock,IsReadOnly,Remark");
                    sb.Append(" FROM tDistrict");
                }
                else if (type == 1)
                {
                    sb.Append("SELECT SysCode,Name,StdCode  FROM tDistrict");
                }

                if (!whereSql.Equals(""))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                }
                if (!orderBySql.Equals(""))
                {
                    sb.Append(" ORDER BY ");
                    sb.Append(orderBySql);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "District" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["District"];
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsDistrict",e);
                errMsg = e.Message;
            }

			return dt;
		}

		/// <summary>
		/// 插入一条明细记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
        public int Insert(clsDistrict model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length = 0;
                sb.Append("INSERT INTO tDistrict(SysCode,StdCode,Name,ShortCut,DistrictIndex,CheckLevel,IsLocal,IsReadOnly,IsLock,Remark)");
                sb.Append(" VALUES(");
                sb.AppendFormat("'{0}',", model.SysCode);
                sb.AppendFormat("'{0}',", model.StdCode);
                sb.AppendFormat("'{0}',", model.Name);
                sb.AppendFormat("'{0}',", model.ShortCut);
                sb.AppendFormat("{0},", model.DistrictIndex);
                sb.AppendFormat("'{0}',", model.CheckLevel);
                sb.AppendFormat("{0},", model.IsLocal);
                sb.AppendFormat("{0},", model.IsReadOnly);
                sb.AppendFormat("{0},", model.IsLock);
                sb.AppendFormat("'{0}'", model.Remark);
                sb.Append(")");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("添加clsDistrict",e);
                errMsg = e.Message;
            }

            return rtn;
        }
        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lev"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetMaxNO(string code, int lev, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn;

            try
            {
                sb.Length = 0;
                sb.Append("SELECT SYSCODE FROM TDISTRICT WHERE SYSCODE LIKE");
                sb.AppendFormat(" '{0}'", code  );
                sb.Append(" ORDER BY SYSCODE DESC");
                Object o = DataBase.GetOneValue(sb.ToString(), out errMsg);
                sb.Length = 0;
                if (o == null)
                {
                    rtn = 0;
                }
                else
                {
                    string rightStr = o.ToString().Substring(o.ToString().Length - lev, lev);
                    rtn = Convert.ToInt32(rightStr);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                rtn = -1;
            }

            return rtn;
        }

        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lev"></param>
        /// <returns></returns>
        public bool CanDelete(string code, int lev)
        {
            string errMsg = string.Empty;

            try
            {
                sb.Length = 0;
                sb.Append("SELECT SYSCODE FROM TDISTRICT WHERE SYSCODE LIKE ");
                sb.AppendFormat("'{0}'", code );
                sb.Append(" ORDER BY SYSCODE DESC");
                Object o = DataBase.GetOneValue(sb.ToString(), out errMsg);
                if (o == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return false;
            }
        }

        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool CanDelete(string code)
        {
            string errMsg = string.Empty;

            try
            {
                sb.Length = 0;
                sb.Append("SELECT TDISTRICT.SYSCODE FROM TDISTRICT,TCOMPANY");
                sb.AppendFormat(" WHERE tDistrict.SysCode=tCompany.DistrictCode and tDistrict.syscode='{0}'", code);
                sb.Append(" ORDER BY TDISTRICT.SYSCODE DESC");
                Object o = DataBase.GetOneValue(sb.ToString(), out errMsg);
                sb.Length = 0;
                sb.Append("SELECT TDISTRICT.SYSCODE FROM TDISTRICT,TUSERUNIT");
                sb.AppendFormat(" WHERE TDISTRICT.SYSCODE=TUSERUNIT.CHECKITEMCODE AND TDISTRICT.SYSCODE='{0}'", code);
                sb.Append(" ORDER BY TDISTRICT.SYSCODE DESC");
                Object o2 = DataBase.GetOneValue(sb.ToString(), out errMsg);
                sb.Length = 0;
                if (o == null && o2 == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return false;
            }
        }

        public static string NameFromCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(""))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT NAME FROM TDISTRICT WHERE SYSCODE='{0}' ORDER BY SYSCODE", code);
                Object obj = DataBase.GetOneValue(sql, out errMsg);
                if (obj == null)
                {
                    return string.Empty;
                }
                else
                {
                    return obj.ToString();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }	

        /// <summary>
        /// 获取行政机构层次字符串
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lev"></param>
        /// <returns></returns>
		public static string LevelNamesFromCode(string code,int lev)
		{	
			string errMsg=string.Empty; 
			string ret="";
			if(code.Equals(""))
			{
				return string.Empty;
			}

            try
            {
                int mod = code.Length / lev;
                for (int i = 1; i < mod; i++)
                {
                    if (i > 1)
                    {
                        //ret += ShareOption.SplitStr;
                    }

                    ret += clsDistrictOpr.NameFromCode(code.Substring(0, lev * i));
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }

            return ret;
		}
	
        /// <summary>
        /// 判断是否最大
        /// </summary>
        /// <param name="strCode"></param>
        /// <returns></returns>
		public static bool DistrictIsMX(string strCode)
		{
			string errMsg=string.Empty; 
			if(strCode.Equals(""))
			{
				return false;
			}

			try
			{//
				string sql = string.Format("SELECT SYSCODE FROM TDISTRICT WHERE SYSCODE='{0}' AND SYSCODE NOT IN (SELECT SYSCODE FROM TDISTRICT AS A WHERE EXISTS (SELECT SYSCODE FROM TDISTRICT  WHERE LEFT(SYSCODE, LEN(A.SYSCODE)) = A.SYSCODE AND SYSCODE <> A.SYSCODE))", strCode);
				//string sql = string.Format("SELECT SYSCODE FROM TDISTRICT WHERE LEN(SYSCODE) < {0}", strCode.Length);
				Object o=DataBase.GetOneValue(sql,out errMsg);
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
				errMsg=e.Message;
				return false;
			}		
		}
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="swhere"></param>
        /// <returns></returns>
        public static bool DistrictIsExist(string swhere)
        {
            string sErrMsg = string.Empty;
            if (swhere.Equals(""))
            {
                return false;
            }

            try
            {
                string sql = "SELECT SYSCODE FROM TDISTRICT WHERE " + swhere;
                Object o = DataBase.GetOneValue(sql, out sErrMsg);
                if (o == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                sErrMsg = e.Message;
                return false;
            }
        }
	}
}
