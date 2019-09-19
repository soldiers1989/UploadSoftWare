using System;
using System.Data;
using System.Text;

namespace DY.FoodClientLib
{
	/// <summary>
	/// 用户单位
	/// </summary>
	public class clsUserUnitOpr
	{
		public clsUserUnitOpr()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        StringBuilder sb = new StringBuilder();

		/// <summary>
		/// 部分修改保存
		/// </summary>
		/// <param name="model">对象clsUserUnit的一个实例参数</param>
		/// <returns></returns>
        public int UpdatePart(clsUserUnit model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append(" UPDATE tUserUnit SET ");
                sb.AppendFormat(" StdCode='{0}'", model.StdCode);
                sb.AppendFormat(",CompanyID='{0}'", model.CompanyID);
                sb.AppendFormat(",FullName='{0}'", model.FullName);
                sb.AppendFormat(",ShortName='{0}'", model.ShortName);
                sb.AppendFormat(",DisplayName='{0}'", model.DisplayName);
                sb.AppendFormat(",ShortCut='{0}'", model.ShortCut);
                sb.AppendFormat(",DistrictCode='{0}'", model.DistrictCode);
                sb.AppendFormat(",PostCode='{0}'", model.PostCode);
                sb.AppendFormat(",Address='{0}'", model.Address);
                sb.AppendFormat(",LinkMan='{0}'", model.LinkMan);
                sb.AppendFormat(",LinkInfo='{0}'", model.LinkInfo);
                sb.AppendFormat(",WWWInfo='{0}'", model.WWWInfo);
                sb.Append(",IsReadOnly=");
                sb.Append(model.IsReadOnly);

                sb.Append(",IsLock=");
                sb.Append(model.IsLock);

                sb.AppendFormat(",Remark='{0}'", model.Remark);
                sb.AppendFormat(" WHERE SysCode='{0}'", model.SysCode);

                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("更新clsUserUnit",e);
                errMsg = e.Message;
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
            sb.Length = 0;
			try
			{
                sb.Append("DELETE FROM tUserUnit WHERE SysCode='");
                sb.Append(mainkey);
                sb.Append("'");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                //string deleteSql="delete from tUserUnit"
                //    + " where SysCode='" + mainkey + "' ";
				//DataBase.ExecuteCommand(deleteSql,out sErrMsg);

				rtn=1;
			}
			catch(Exception e)
			{
				//Log.WriteLog("通过主键删除clsUserUnit",e);
				errMsg=e.Message;;
			}

			return rtn;
		}
		
		/// <summary>
		/// 根据查询串条件查询记录
		/// </summary>
		/// <param name="whereSql">查询条件串,不含Where</param>
		/// <param name="orderBySql">排序串,不含Order</param>
        /// <param name="type">查询类别</param>
		/// <returns></returns>
        public DataTable GetAsDataTable(string whereSql, string orderBySql, int type)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;

            try
            {
                sb.Length = 0;
                if (type == 0)
                {
                    sb.Append("SELECT A.SYSCODE,A.STDCODE,A.FULLNAME,A.SHORTNAME,A.DISPLAYNAME,A.SHORTCUT,A.DISTRICTCODE,B.NAME AS DISTRICTNAME,A.POSTCODE,A.ADDRESS,A.LINKMAN,A.LINKINFO,A.WWWINFO,A.ISREADONLY,A.ISLOCK,A.REMARK,A.CompanyId FROM TUSERUNIT AS A LEFT JOIN TDISTRICT AS B ON A.DISTRICTCODE=B.SYSCODE ");
                }
                else if (type == 1)
                {
                    sb.Append("SELECT FULLNAME,STDCODE,SYSCODE,CompanyID FROM TUSERUNIT");
                }

                if (!whereSql.Equals(string.Empty))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                }
                if (!orderBySql.Equals(string.Empty))
                {
                    sb.Append(" ORDER BY ");
                    sb.Append(orderBySql);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "UserUnit" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["UserUnit"];
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return dtbl;
        }

		/// <summary>
		/// 插入一条明细记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
        public int Insert(clsUserUnit model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length = 0;
                sb.Append("INSERT INTO tUserUnit(SysCode,StdCode,FullName,ShortName,DisplayName,ShortCut,DistrictCode,PostCode,Address,LinkMan,LinkInfo,WWWInfo,IsReadOnly,IsLock,Remark,CompanyID)");
                sb.Append(" VALUES(");
                sb.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{7}','{8}','{9}','{10}','{11}',{12},{13},'{14}','{15}'", model.SysCode, model.StdCode, model.FullName, model.ShortName, model.DisplayName, model.ShortCut, model.DistrictCode, model.PostCode, model.Address, model.LinkMan, model.LinkInfo, model.WWWInfo, model.IsReadOnly, model.IsLock, model.Remark, model.CompanyID);
                sb.Append(")");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;

                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lev">层次</param>
        /// <param name="sErrMsg"></param>
        /// <returns></returns>
        public int GetMaxNO(string code, int lev, out string sErrMsg)
        {
            sErrMsg = string.Empty;
            int rtn;
            try
            {
                string sql = string.Format("SELECT syscode FROM tUserUnit WHERE syscode LIKE '{0}' ORDER BY syscode DESC", code
                    + StringUtil.RepeatChar('_', lev));
                object obj = DataBase.GetOneValue(sql, out sErrMsg);
                if (obj == null)
                {
                    rtn = 0;
                }
                else
                {
                    string rightStr = obj.ToString().Substring(obj.ToString().Length - lev, lev);
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

        /// <summary>
        /// 通过编码获取检测点目标字段值
        /// </summary>
        /// <param name="find">目标字段</param>
        /// <param name="code">用户单位编码</param>
        /// <returns></returns>
        public static string GetNameFromCode(string find, string code)
        {
            string errMsg = string.Empty;
            if (string.IsNullOrEmpty(code))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT {0} FROM TUSERUNIT WHERE SYSCODE='{1}' ORDER BY SYSCODE", find, code);
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

        /// <summary>
        /// 通过代码获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
		public static string GetNameFromCode(string code)
		{
			return clsUserUnitOpr.GetNameFromCode("FullName",code);
		}

        /// <summary>
        /// 通过标准代码获取名称
        /// </summary>
        /// <param name="code">单位代码</param>
        /// <returns></returns>
        public static string GetNameFromStandarCode(string code)
        {
            string sErrMsg = string.Empty;
            if (code.Equals(string.Empty))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT FullName FROM tUserUnit WHERE stdcode='{0}' ORDER BY syscode", code);
                Object obj = DataBase.GetOneValue(sql, out sErrMsg);
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
                sErrMsg = e.Message;
                return null;
            }
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool ExistCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(string.Empty))
            {
                return false;
            }
            try
            {
                string sql = string.Format("SELECT SYSCODE FROM TUSERUNIT WHERE SYSCODE='{0}'", code);
                object obj = DataBase.GetOneValue(sql, out errMsg);
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
                return false;
            }
        }
        /// <summary>
        /// 获取标准代码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetStdCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(string.Empty))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT STDCODE FROM TUSERUNIT WHERE SYSCODE='{0}'", code);
                object obj = DataBase.GetOneValue(sql, out errMsg);
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
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取检测点类型
        /// </summary>
        /// <returns></returns>
        public string GetCheckPointType()
        {
            string errMsg = string.Empty;

            try
            {
                string sql = "SELECT SHORTCUT FROM TUSERUNIT ORDER BY SYSCODE";
                object obj = DataBase.GetOneValue(sql, out errMsg);
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
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取父类数据集
        /// </summary>
        /// <param name="pType"></param>
        /// <returns></returns>
        public DataTable GetParents(out int pType)
        {
            string errMsg = string.Empty;
            pType = 0;
            DataTable dt = null;

            try
            {
                string sql = "SELECT DistrictCode FROM TUSERUNIT ORDER BY SYSCODE";
                object obj = DataBase.GetOneValue(sql, out errMsg);
                if (obj == null)
                {
                    pType = -1;
                }
                else
                {
                    string selectSql = "SELECT Name AS DistrictName,syscode FROM tDistrict WHERE syscode LIKE '" + obj.ToString() + "___'"
                        + " And IsReadOnly = TRUE And IsLock = FALSE";
                    string[] cmd = new string[1] { selectSql };
                    string[] names = new string[1] { "DistrictName" };
                    dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["DistrictName"];
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }
	}
}
