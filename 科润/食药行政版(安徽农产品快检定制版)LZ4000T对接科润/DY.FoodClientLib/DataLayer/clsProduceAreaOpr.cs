using System;
using System.Data;
using System.Text;

namespace DY.FoodClientLib
{
	/// <summary>
	/// 产品产地
	/// </summary>
	public class clsProduceAreaOpr
	{
		public clsProduceAreaOpr()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        private StringBuilder sb = new StringBuilder();
		/// <summary>
		/// 部分修改保存
		/// </summary>
		/// <param name="model">对象clsProduceArea的一个实例参数</param>
		/// <returns></returns>
        public int UpdatePart(clsProduceArea model, int lev, out string errMsg)
        {
            sb.Length = 0;
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Append("UPDATE TPRODUCEAREA SET ");
                sb.AppendFormat("StdCode='{0}',", model.StdCode);
                sb.AppendFormat("Name='{0}',", model.Name);
                sb.AppendFormat("ShortCut='{0}',", model.ShortCut);
                sb.AppendFormat("DistrictIndex={0},", model.DistrictIndex);
                sb.AppendFormat("CheckLevel='{0}',", model.CheckLevel);
                sb.AppendFormat("IsLocal={0},", model.IsLocal);
                sb.AppendFormat("IsReadOnly={0},", model.IsReadOnly);
                sb.AppendFormat("IsLock={0},", model.IsLock);
                sb.AppendFormat("Remark='{0}'", model.Remark);
                sb.AppendFormat(" WHERE SysCode='{0}'", model.SysCode);

                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;

                if (model.IsLocal)
                {
                    sb.Append("UPDATE TPRODUCEAREA SET");
                    sb.AppendFormat(" IsLocal={0}", model.IsLocal);
                    sb.AppendFormat(" WHERE SYSCODE LIKE '{0}%'", model.SysCode);
                    DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                    sb.Length = 0;
                }

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("更新clsProduceArea",e);
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
		public int Delete(string whereSql,out string errMsg)
		{
			errMsg=string.Empty;        
			int rtn = 0;
            sb.Length = 0;
			try
			{
                sb.Append("DELETE FROM TPRODUCEAREA");

				if(!whereSql.Equals(""))
				{
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
					//deleteSql+= " where " + whereSql;
				}
				DataBase.ExecuteCommand(sb.ToString(),out errMsg);
                sb.Length = 0;

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
        public int DeleteByPrimaryKey(string mainkey, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.AppendFormat("DELETE FROM TPRODUCEAREA WHERE SYSCODE='{0}'", mainkey);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("通过主键删除clsProduceArea",e);
                errMsg = e.Message;
            }

            return rtn;
        }
		
		/// <summary>
		/// 根据查询串条件查询记录
		/// </summary>
		/// <param name="whereSql">查询条件串,不含Where</param>
		/// <param name="orderBySql">排序串,不含Order</param>
		/// <returns></returns>
        public DataTable GetAsDataTable(string whereSql, string orderBySql, int type)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;
            sb.Length = 0;
            try
            {
                //string selectSql="";
                if (type == 0)
                {
                    sb.Append("SELECT SysCode,StdCode,Name,ShortCut,DistrictIndex,CheckLevel,");
                    sb.Append("IsLocal,IsLock,IsReadOnly,Remark");
                    sb.Append(" FROM tProduceArea");
                }
                else if (type == 1)
                {
                    sb.Append("SELECT SYSCODE,NAME,STDCODE FROM TPRODUCEAREA");
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
                string[] names = new string[1] { "ProduceArea" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ProduceArea"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsProduceArea",e);
                errMsg = e.Message;
            }

            return dtbl;
        }

		/// <summary>
		/// 插入一条明细记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
        public int Insert(clsProduceArea model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            sb.Length = 0;
            try
            {
                sb.Append("INSERT INTO tProduceArea");
                sb.Append("(SysCode,StdCode,Name,ShortCut,DistrictIndex,CheckLevel,IsLocal,IsReadOnly,IsLock,Remark)");
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
                //Log.WriteLog("添加clsProduceArea",e);
                errMsg = e.Message;
            }

            return rtn;
        }
        /// <summary>
        /// 获取最大编码
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lev"></param>
        /// <param name="sErrMsg"></param>
        /// <returns></returns>
        public int GetMaxNO(string code, int lev, out string sErrMsg)
        {
            sErrMsg = string.Empty;
            int rtn;

            try
            {
                string sql = string.Format("SELECT SYSCODE FROM TPRODUCEAREA WHERE SYSCODE LIKE '{0}' ORDER BY SYSCODE DESC", code
                    + StringUtil.RepeatChar('_', lev));
                Object obj = DataBase.GetOneValue(sql, out sErrMsg);
                if (obj == null)
                {
                    rtn = 0;
                }
                else
                {
                    string temp = obj.ToString();
                    string rightStr =temp.Substring(temp.Length - lev, lev);
                    rtn = Convert.ToInt32(rightStr);
                }
            }
            catch (Exception e)
            {
                sErrMsg = e.Message;
                rtn = -1;
            }

            return rtn;
        }

		public bool CanDelete(string code,int lev)
		{
			string errMsg=string.Empty; 

			try
			{
                string sql = string.Format("SELECT SYSCODE FROM TPRODUCEAREA WHERE SYSCODE LIKE '{0}' ORDER BY SYSCODE DESC", code
                    + StringUtil.RepeatChar('_', lev));
				Object obj=DataBase.GetOneValue(sql,out errMsg);
				if(obj==null)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch(Exception e)
			{
				errMsg=e.Message;
				return false;
			}
		}

        public bool CanDelete(string code)
        {
            string sErrMsg = string.Empty;

            try
            {
                string sql = string.Format("SELECT TPRODUCEAREA.SYSCODE FROM TPRODUCEAREA,TCOMPANY WHERE tProduceArea.SysCode=tCompany.DistrictCode AND tProduceArea.syscode='{0}' ORDER BY TPRODUCEAREA.SYSCODE DESC", code);
                Object o = DataBase.GetOneValue(sql, out sErrMsg);

                //sql="select tProduceArea.syscode from tProduceArea,tUserUnit"
                //        + " where tProduceArea.SysCode=tUserUnit.CheckItemCode and tProduceArea.syscode='" + code 
                //        + "' order by tProduceArea.syscode desc";

                sql = string.Format("SELECT TPRODUCEAREA.SYSCODE FROM tProduceArea,tUserUnit where tProduceArea.SysCode=tUserUnit.SysCode and tProduceArea.syscode='{0}' ORDER BY TPRODUCEAREA.SYSCODE DESC", code);

                Object o2 = DataBase.GetOneValue(sql, out sErrMsg);
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
                sErrMsg = e.Message;
                return false;
            }
        }
        /// <summary>
        /// 通过代码获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string NameFromCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(""))
            {
                return "";
            }

            try
            {
                string sql = string.Format("SELECT NAME FROM TPRODUCEAREA WHERE SYSCODE='{0}' ORDER BY SYSCODE", code);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                if (o == null)
                {
                    return string.Empty;
                }
                else
                {
                    return o.ToString();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }

        public static string LevelNamesFromCode(string code, int lev)
        {
            string sErrMsg = string.Empty;
            string sReturn = string.Empty;
            if (code.Equals(""))
            {
                return "";
            }

            try
            {
                int iMod = code.Length / lev;
                for (int i = 1; i < iMod; i++)
                {
                    if (i > 1)
                    {
                        sReturn += ShareOption.SplitStr;
                    }

                    sReturn += clsProduceAreaOpr.NameFromCode(code.Substring(0, lev * i));
                }
            }
            catch (Exception e)
            {
                sErrMsg = e.Message;
                return null;
            }

            return sReturn;
        }	
	}
}
