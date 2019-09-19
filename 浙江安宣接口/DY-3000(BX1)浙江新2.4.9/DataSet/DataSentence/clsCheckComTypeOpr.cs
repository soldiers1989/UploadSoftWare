using System;
using System.Data;
using System.Text;

namespace DYSeriesDataSet
{
    /// <summary>
    /// 检测单位数据操作类
    /// 原作者：徐磊
    /// 很多垃圾代码，需要改进
    /// 备注：原作者写没有任何注释
    /// </summary>
    public class clsCheckComTypeOpr
    {
        public clsCheckComTypeOpr()
        {
        }
        private StringBuilder sql = new StringBuilder();
        /// <summary>
        /// 部分修改保存
        /// </summary>
        /// <param name="model">对象clsCheckComType的一个实例参数</param>
        /// <returns></returns>
        public int UpdatePart(clsCheckComType model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sql.Length = 0;
                sql.Append("UPDATE TCHECKCOMTYPE SET");
                sql.AppendFormat(" TypeName='{0}'", model.TypeName);
                sql.AppendFormat(",NameCall='{0}'", model.NameCall);
                sql.AppendFormat(",AreaCall='{0}'", model.AreaCall);
                sql.AppendFormat(",VerType='{0}'", model.VerType);
                sql.AppendFormat(",IsReadOnly={0}", model.IsReadOnly);
                sql.AppendFormat(",IsLock={0}", model.IsLock);
                sql.AppendFormat(",ComKind='{0}'", model.ComKind);
                sql.AppendFormat(",AreaTitle='{0}'", model.AreaTitle);
                sql.AppendFormat(",NameTitle='{0}'", model.NameTitle);
                sql.AppendFormat(",DomainTitle='{0}'", model.DomainTitle);
                sql.AppendFormat(",SampleTitle='{0}'", model.SampleTitle);
                sql.Append(" WHERE ID=");
                sql.Append(model.ID);


                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// 部分修改保存
        /// </summary>
        /// <param name="model">对象clsCheckComType的一个实例参数</param>
        /// <returns></returns>
        public int UpdatePartTag(clsCheckComType model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sql.Length = 0;
                sql.Append("UPDATE TCHECKCOMTYPE SET");

                sql.AppendFormat(" NameCall='{0}'", model.NameCall);
                sql.AppendFormat(",AreaCall='{0}'", model.AreaCall);
                sql.AppendFormat(",AreaTitle='{0}'", model.AreaTitle);
                sql.AppendFormat(",NameTitle='{0}'", model.NameTitle);
                sql.AppendFormat(",DomainTitle='{0}'", model.DomainTitle);
                sql.AppendFormat(",SampleTitle='{0}'", model.SampleTitle);
                sql.Append(" WHERE ID=");
                sql.Append(model.ID);


                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="errMsg">输出错误信息</param>
        /// <returns></returns>
        public int Delete(string strWhere, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sql.Length = 0;
                sql.Append("DELETE FROM tcheckcomtype ");
                // string deleteSql = "delete from tcheckcomtype";

                if (strWhere != string.Empty)
                {
                    sql.Append(" WHERE ");
                    sql.Append(strWhere);
                    //deleteSql += " where " + whereSql;
                }
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;

                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// 根据主键编号删除记录
        /// </summary>
        /// <param name="%pkname%">主键编号</param>
        /// <returns></returns>
        public int DeleteByPrimaryKey(string mainkey, out string sErrMsg)
        {
            sErrMsg = string.Empty;
            int rtn = 0;

            try
            {
                string deleteSql = string.Format("DELETE FROM tcheckcomtype WHERE ID={0}", mainkey);
                DataBase.ExecuteCommand(deleteSql, out sErrMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("通过主键删除clsCheckType",e);
                sErrMsg = e.Message; ;
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
            sql.Length = 0;
            try
            {
                string selectSql = string.Empty;
                if (type == 0)
                {
                    sql.Append("SELECT A.ID,A.TypeName,A.NameCall,A.AreaCall,A.VerType,A.IsLock,A.IsReadOnly,A.ComKind,B.Name As ComKindName,A.AreaTitle,A.NameTitle,A.DomainTitle,A.SampleTitle from tcheckcomtype As A Left Join tCompanyKind As B On A.ComKind=B.SysCode");
                }
                else if (type == 1)
                {
                    sql.Append("SELECT TypeName from tcheckcomtype");
                }
                else if (type == 2)
                {
                    sql.Append("SELECT TypeName,NameCall,AreaCall,AreaTitle,NameTitle,DomainTitle,SampleTitle FROM tcheckcomtype ");
                }
                else if (type == 3)
                {
                    sql.Append("SELECT TOP 1 * FROM tcheckcomtype ");
                }
                if (!whereSql.Equals(""))
                {
                    sql.Append(" WHERE ");
                    sql.Append(whereSql);
                    //selectSql += " where " + whereSql;
                }
                if (!orderBySql.Equals(""))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(orderBySql);
                    //selectSql += " order by " + orderBySql;
                }
                string[] cmd = new string[1] { sql.ToString() };
                //sCmd[0] = selectSql;
                string[] names = new string[1] { "CheckComType" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["CheckComType"];
                sql.Length = 0;
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
        public int Insert(clsCheckComType model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sql.Length = 0;
                sql.Append("INSERT INTO tcheckcomtype(TypeName,NameCall,AreaCall,VerType,IsReadOnly,IsLock,ComKind,AreaTitle,NameTitle,DomainTitle,SampleTitle)VALUES('");
                sql.Append(model.TypeName);
                sql.Append("','");
                sql.Append(model.NameCall);
                sql.Append("','");
                sql.Append(model.AreaCall);
                sql.Append("','");
                sql.Append(model.VerType);
                sql.Append("',");
                sql.Append(model.IsReadOnly);
                sql.Append(",");
                sql.Append(model.IsLock);
                sql.Append(",'");
                sql.Append(model.ComKind);
                sql.Append("','");
                sql.Append(model.AreaTitle);
                sql.Append("','");
                sql.Append(model.NameTitle);
                sql.Append("','");
                sql.Append(model.DomainTitle);
                sql.Append("','");
                sql.Append(model.SampleTitle);
                sql.Append("')");
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("添加clsCheckType",e);
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool TypeNameIsExist(string name)
        {
            string errMsg = string.Empty;
            if (name.Equals(""))
            {
                return false;
            }

            try
            {
                string sql = string.Format("SELECT TYPENAME FROM tCheckComType WHERE TypeName='{0}'", name);
                Object o = DataBase.GetOneValue(sql, out errMsg);
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
                errMsg = e.Message;
                return false;
            }
        }
        /// <summary>
        /// 判断是否能被删除
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool TypeIsNoDel(string name)
        {
            string errMsg = string.Empty;
            if (name.Equals(""))
            {
                return false;
            }

            try
            {
                string sql = string.Format("SELECT SYSCODE FROM TUSERUNIT WHERE SHORTCUT='{0}'", name);
                Object o = DataBase.GetOneValue(sql, out errMsg);
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
                errMsg = e.Message;
                return false;
            }
        }

        public static string ValueFromName(string file, string name)
        {
            string errMsg = string.Empty;
            if (name.Equals("") || file.Equals(""))
            {
                return "";
            }

            try
            {
                string sql = string.Format("SELECT {0} FROM TCHECKCOMTYPE WHERE TYPENAME='{1}'", file, name);
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
    }
}
