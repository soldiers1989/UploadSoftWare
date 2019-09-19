using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;


namespace DY.FoodClientLib
{
	/// <summary>
	/// 食品类别操作类
	/// </summary>
	public class clsFoodClassOpr
	{
		public clsFoodClassOpr()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
       private StringBuilder sb = new StringBuilder();
		/// <summary>
		/// 部分修改保存
		/// </summary>
		/// <param name="model">对象clsFoodClass的一个实例参数</param>
		/// <returns></returns>
		public int UpdatePart(clsFoodClass model,out string errMsg)
		{
			errMsg=string.Empty; 
			int rtn=0;

            try
            {

                sb.Length = 0;
                sb.AppendFormat("UPDATE tFoodClass SET StdCode='{0}'", model.StdCode);
                sb.AppendFormat(",Name='{0}'", model.Name);
                sb.AppendFormat(",ShortCut='{0}'", model.ShortCut);
                sb.AppendFormat(",CheckLevel='{0}'", model.CheckLevel);
                sb.AppendFormat(",CheckItemCodes='{0}'", model.CheckItemCodes);
                sb.AppendFormat(",CheckItemValue='{0}'", model.CheckItemValue);
                sb.Append(",IsReadOnly=");
                sb.Append(model.IsReadOnly);
                sb.Append(",IsLock=");
                sb.Append(model.IsLock);
                sb.AppendFormat(",Remark='{0}'", model.Remark);
                sb.AppendFormat(",FoodProperty='{0}'", model.FoodProperty);
                sb.AppendFormat(" WHERE SysCode='{0}'", model.SysCode);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("更新clsFoodClass",e);
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

			try
			{			
				string deleteSql="DELETE FROM TFOODCLASS";

				if(!whereSql.Equals(""))
				{
                    deleteSql += string.Format(" WHERE {0} ", whereSql);
				}
				DataBase.ExecuteCommand(deleteSql,out errMsg);

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
                string deleteSql = string.Format("DELETE FROM tfoodclass WHERE SysCode='{0}' ", mainkey);
                DataBase.ExecuteCommand(deleteSql, out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("通过主键删除clsFoodClass",e);
                errMsg = e.Message; ;
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

            try
            {
                sb.Length = 0;
                //string selectSql = string.Empty;
                if (type == 0)
                {
                    sb.Append("SELECT SysCode,StdCode,Name,ShortCut,CheckLevel,CheckItemCodes");
                    sb.Append(",CheckItemValue,IsLock,IsReadOnly,Remark,FoodProperty FROM tfoodclass");
                }
                else if (type == 1)
                {
                    sb.Append("SELECT SysCode,Name,StdCode FROM tfoodclass");
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
                    sb.Append(" ASC ");
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "foodclass" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["foodclass"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsFoodClass",e);
                errMsg = e.Message;
            }

            return dtbl;
        }

        /// <summary>
        /// 根据查询串条件查询记录
        /// </summary>
        /// <param name="whereSql">查询条件串,不含Where</param>
        /// <param name="orderBySql">排序串,不含Order</param>
        /// <returns></returns>
        public DataTable GetTreeListTable(string whereSql, string orderBySql)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;

            try
            {
                sb.Length = 0;

                sb.Append("SELECT SysCode,Name FROM tfoodclass WHERE IsLock=false AND IsReadOnly=true AND LEN(SysCode)=10 AND LEN(CheckItemCodes)>0 ");
                sb.Append(" UNION ");
                sb.Append(" SELECT SysCode,Name FROM tfoodclass WHERE IsLock=false AND IsReadOnly=true AND LEN(SysCode)>10 AND LEN(CheckItemCodes)>0 ");
               
                if (!whereSql.Equals(""))
                {
                    sb.Append(" AND ");
                    sb.Append(whereSql);
                }
                if (!orderBySql.Equals(""))
                {
                    sb.Append(" ORDER BY ");
                    sb.Append(orderBySql);
                    sb.Append(" ASC ");
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "foodclass" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["foodclass"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsFoodClass",e);
                errMsg = e.Message;
            }

            return dtbl;
        }


		/// <summary>
		/// 插入一条明细记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
        public int Insert(clsFoodClass model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length = 0;
                sb.Append("INSERT INTO TFOODCLASS");
                sb.Append("(SysCode,StdCode,Name,ShortCut,CheckLevel,CheckItemCodes");
                sb.Append(",CheckItemValue,IsReadOnly,IsLock,Remark,FoodProperty)");
                sb.Append("VALUES(");
                sb.AppendFormat("'{0}',", model.SysCode);
                sb.AppendFormat("'{0}',", model.StdCode);
                sb.AppendFormat("'{0}',", model.Name);
                sb.AppendFormat("'{0}',", model.ShortCut);
                sb.AppendFormat("'{0}',", model.CheckLevel);
                sb.AppendFormat("'{0}',", model.CheckItemCodes);
                sb.AppendFormat("'{0}',", model.CheckItemValue);
                sb.AppendFormat("{0},", model.IsReadOnly);
                sb.AppendFormat("{0},", model.IsLock);
                sb.AppendFormat("'{0}',", model.Remark);
                sb.AppendFormat("'{0}'", model.FoodProperty);
                sb.Append(")");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("添加clsFoodClass",e);
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// 获取最大值
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
                string sql = string.Format("SELECT SYSCODE FROM tfoodclass WHERE syscode LIKE '{0}' ORDER BY SYSCODE DESC", code + StringUtil.RepeatChar('_', lev));
                Object o = DataBase.GetOneValue(sql, out errMsg);
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
		public bool CanDelete(string code,int lev)
		{
			string errMsg=string.Empty; 

			try
			{
                string sql = string.Format("SELECT SYSCODE FROM TFOODCLASS WHERE SYSCODE LIKE '{0}' ORDER BY SYSCODE DESC", code
                    + StringUtil.RepeatChar('_', lev));
				Object o=DataBase.GetOneValue(sql,out errMsg);
				if(o==null)
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

        /// <summary>
        /// 通过代码获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
		public static string NameFromCode(string code)
		{
            if (string.IsNullOrEmpty(code))
            {
                return string.Empty;
            }
			string errMsg=string.Empty;


            try
            {
                string sql = string.Format("SELECT NAME FROM TFOODCLASS WHERE SYSCODE='{0}' ORDER BY SYSCODE", code);
                Object obj = DataBase.GetOneValue(sql, out errMsg);
                if (obj == null || obj == DBNull.Value)
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

        public static string CodeFromName(string name, string checkitemcode)
        {
            string errMsg = string.Empty;
            if (name.Equals(""))
            {
                return string.Empty;
            }

            try
            {
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("SELECT SYSCODE FROM tfoodclass WHERE Name=");
                sbSql.Append("'");
                sbSql.Append(name);
                sbSql.Append("'");
                sbSql.Append(" AND CHECKITEMCODES LIKE ");
                sbSql.Append("'%{");
                sbSql.Append(checkitemcode);
                sbSql.Append(":%' ORDER BY SYSCODE");
                Object obj = DataBase.GetOneValue(sbSql.ToString(), out errMsg);
                sbSql.Length = 0;
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

        public static string[] ValueFromCode(string foodCode, string checkItemCode)
        {
            string[] rtn = new string[3];
            rtn[0] = "";
            rtn[1] = "0";
            rtn[2] = "";
            bool IsExist = false;
            string errMsg = string.Empty;
            if (foodCode.Equals("") || checkItemCode.Equals(""))
            {
                return rtn;
            }
            try
            {
                string sql = "SELECT CheckItemCodes FROM tfoodclass WHERE syscode='" + foodCode + "'";
                Object o = DataBase.GetOneValue(sql, out errMsg);
                if (o == null)
                {
                    return rtn;
                }
                else
                {
                    string[,] result = StringUtil.GetFoodAry(o.ToString());
                    if (result.GetLength(0) >= 1)
                    {
                        for (int i = 0; i < result.GetLength(0); i++)
                        {
                            if (result[i, 0].Equals(checkItemCode))
                            {
                                IsExist = true;
                                if (result[i, 1].Equals("-1"))
                                {
                                    sql = "SELECT DemarcateInfo FROM tCheckItem WHERE SysCode='" + checkItemCode + "'";
                                    o = DataBase.GetOneValue(sql, out errMsg);
                                    if (o == null)
                                    {
                                        rtn[0] = "";
                                    }
                                    else
                                    {
                                        rtn[0] = o.ToString();
                                    }
                                    sql = "SELECT StandardValue FROM tCheckItem WHERE SysCode='" + checkItemCode + "'";
                                    o = DataBase.GetOneValue(sql, out errMsg);
                                    if (o == null)
                                    {
                                        rtn[1] = "";
                                    }
                                    else
                                    {
                                        rtn[1] = o.ToString();
                                    }
                                    sql = "SELECT Unit FROM tCheckItem WHERE SysCode='" + checkItemCode + "'";
                                    o = DataBase.GetOneValue(sql, out errMsg);
                                    if (o == null)
                                    {
                                        rtn[2] = "";
                                    }
                                    else
                                    {
                                        rtn[2] = o.ToString();
                                    }
                                }
                                else
                                {
                                    rtn[0] = result[i, 1];
                                    rtn[1] = result[i, 2];
                                    rtn[2] = result[i, 3];
                                }
                                break;
                            }
                        }
                    }
                    if (!IsExist)
                    {
                        rtn[0] = "-1";
                        rtn[1] = "0";
                        rtn[2] = "-1";
                    }
                    return rtn;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return rtn;
            }
        }

        public static bool ExistCheckItem(string precode, string code, string checkcode, out string sErrMsg)
        {
            try
            {
                string sql = "SELECT Count(*) From tFoodClass Where SysCode Like '" + precode + StringUtil.RepeatChar('_', ShareOption.FoodCodeLevel)
                    + "' And SysCode<>'" + code + "' And CheckItemCodes Like '%{" + checkcode + ":%}%'";
                Object o = DataBase.GetOneValue(sql, out sErrMsg);
                if (o == null)
                {
                    return false;
                }
                else
                {
                    if (Convert.ToInt32(o.ToString()) >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                sErrMsg = e.Message;
                return false;
            }
        }

		public static void DelCheckItem(string code,string checkcode,out string sErrMsg)
		{
			try
			{
				string pattern=@"({"+checkcode +@":[\S\t]*?})";
				Regex r=new Regex(pattern,System.Text.RegularExpressions.RegexOptions.IgnoreCase); 
				string strResult=string.Empty;
                string sql = string.Format("SELECT CheckItemCodes From tFoodClass Where SysCode='{0}'", code);
				Object obj=DataBase.GetOneValue(sql,out sErrMsg);
                if (obj != null)
                {
                    strResult = r.Replace(obj.ToString(), "");
                    sql = string.Format("UPDATE tFoodClass Set CheckItemCodes='{0}' Where SysCode='{1}'", strResult, code);
                    DataBase.ExecuteCommand(sql, out sErrMsg);
                }
			}
			catch(Exception e)
			{
				sErrMsg=e.Message;
			}
		}

		public static void AddCheckItem(string code,string checkcode,out string errMsg)
		{
			try
			{
				string strResult="{"+checkcode+":-1:-1:-1}";
                string sql = string.Format("Update tFoodClass Set CheckItemCodes=CheckItemCodes+'{0}' Where SysCode='{1}'", strResult, code);
				DataBase.ExecuteCommand(sql,out errMsg);
			}
			catch(Exception e)
			{
				errMsg=e.Message;
			}
		}

		public static void AddAllCheckItem(string preCode,string checkCode,out string errMsg)
		{
			try
			{
				string strResult="{"+checkCode+":-1:-1:-1}";
                string sql = String.Format("Update tFoodClass Set CheckItemCodes=CheckItemCodes+'{0}' Where SysCode Like '{1}%'", strResult, preCode);
				DataBase.ExecuteCommand(sql,out errMsg);
			}
			catch(Exception e)
			{
				errMsg=e.Message;
			}
		}

        public static string EditCheckItem(string code, string checkcode, string newvalue, out string errMsg)
        {
            try
            {
                string pattern = @"({" + checkcode + @":[\S\t]*?})";
                Regex r = new Regex(pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                string strResult = string.Empty;
                string sql = string.Format("SELECT CheckItemCodes From tFoodClass WHERE SysCode='{0}'", code);
                Object obj = DataBase.GetOneValue(sql, out errMsg);
                if (obj != null)
                {
                    strResult = r.Replace(obj.ToString(), newvalue);
                    sql = string.Format("UPDATE tFoodClass SET CheckItemCodes='{0} ' WHERE SysCode='{1}'", strResult, code);
                    DataBase.ExecuteCommand(sql, out errMsg);
                }
                return strResult;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lev"></param>
        /// <returns></returns>
        public static string LevelNamesFromCode(string code, int lev)
        {
            string ret = string.Empty;
            if (code.Equals(string.Empty))
            {
                return string.Empty;
            }
            try
            {
                int mode = code.Length / lev;

                //for(int i=1;i<=iMod;i++)//***yzh2005-11-17begin***
                for (int i = 1; i < mode; i++)//***yzh2005-11-17end***
                {
                    if (i > 1)
                    {
                        ret += ShareOption.SplitStr;
                    }

                    ret += clsFoodClassOpr.NameFromCode(code.Substring(0, lev * i));
                }
            }
            catch //(Exception ex)
            {
                return null;
            }

            return ret;
        }	

        /// <summary>
        /// 判断是否存在相同的名称
        /// </summary>
        /// <param name="name"></param>
        /// <param name="stdCode"></param>
        /// <param name="sysCode"></param>
        /// <returns></returns>
        public bool ExistSameValue(string name, string stdCode, string sysCode)
        {
            string errMsg = string.Empty;

            try
            {
                string sql = string.Empty;
                if (sysCode.Equals(""))
                {
                    sql = string.Format("SELECT Name FROM tfoodclass WHERE Name='{0} ' Or StdCode='{1}'", name, stdCode);
                }
                else
                {
                    sql = string.Format("SELECT Name FROM tfoodclass WHERE (Name='{0}' Or StdCode='{1}') And SysCode<>'{1}'", name, stdCode);
                }
                Object obj = DataBase.GetOneValue(sql, out errMsg);
                if (obj == null)
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
                errMsg = e.Message;
                return true;
            }
        }

        public string GetPreCheckItemCodes(string code, out string errMsg)
        {
            errMsg = string.Empty;
            string rtn = "";

            try
            {
                string sql = string.Format("select CheckItemCodes from tFoodClass where syscode = '{0}' order by syscode desc", code);
                Object obj = DataBase.GetOneValue(sql, out errMsg);
                if (obj == null)
                {
                    rtn = string.Empty;
                }
                else
                {
                    rtn = obj.ToString();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                rtn = string.Empty;
            }

            return rtn;
        }

        /// <summary>
        /// 通过食品类别获取检测项目编号
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string CheckItemsFromCode(string code)
        {
            string sErrMsg = string.Empty;
            if (code.Equals(string.Empty))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("select CheckItemCodes from tFoodClass where syscode='{0}' order by syscode", code);
                Object o = DataBase.GetOneValue(sql, out sErrMsg);
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
                sErrMsg = e.Message;
                return null;
            }
        }

        public string errMsg { get; set; }
    }
}
