using System;
using System.Data;
using System.Text;

namespace DYSeriesDataSet
{
    /// <summary>
    /// 检测类别
    /// </summary>
    public class clsCheckTypeOpr
    {
        public clsCheckTypeOpr()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private StringBuilder sql = new StringBuilder();
        /// <summary>
        /// 部分修改保存
        /// </summary>
        /// <param name="model">对象clsCheckType的一个实例参数</param>
        /// <returns></returns>
        public int UpdatePart(clsCheckType model, string sName, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sql.Length = 0;
                sql.Append("UPDATE TCHECKTYPE SET ");
                sql.AppendFormat("Name='{0}',", model.Name);
                sql.AppendFormat("IsReadOnly={0},", model.IsReadOnly);
                sql.AppendFormat("IsLock={0},", model.IsLock);
                sql.AppendFormat("Remark='{0}'", model.Remark);
                sql.AppendFormat(" WHERE Name='{0}'", sName);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;

                //更新相关表
                //updateSql="update tCheckItem set "
                //    + "CheckType='" + OprObject.Name + "'"
                //    + " where CheckType='" + sName + "' ";
                //DataBase.ExecuteCommand(updateSql,out sErrMsg);

                //updateSql="update tresult set "
                //    + "CheckType='" + OprObject.Name + "'"
                //    + " where CheckType='" + sName + "' ";
                //DataBase.ExecuteCommand(updateSql,out sErrMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("更新clsCheckType",e);
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// 是否可以删除
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int Delete(string whereSql, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sql.Length = 0;
                sql.Append("DELETE FROM TCHECKTYPE");

                if (!whereSql.Equals(""))
                {
                    sql.Append(" WHERE ");
                    sql.Append(whereSql);
                }
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);

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
        public int DeleteByPrimaryKey(string mainkey, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            sql.Length = 0;
            try
            {
                sql.Append("DELETE FROM TCHECKTYPE");
                sql.Append(" WHERE Name='");
                sql.Append(mainkey);
                sql.Append("'");
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("通过主键删除clsCheckType",e);
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
            DataTable dt = null;

            try
            {
                sql.Length = 0;
                if (type == 0)
                {
                    sql.Append("SELECT NAME,ISLOCK,ISREADONLY,REMARK FROM TCHECKTYPE");
                }
                else if (type == 1)
                {
                    sql.Append("SELECT NAME FROM tchecktype");
                }

                if (!whereSql.Equals(""))
                {
                    sql.Append(" WHERE ");
                    sql.Append(whereSql);
                }
                if (!orderBySql.Equals(""))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(orderBySql);
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "CheckType" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["CheckType"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsCheckType",e);
                errMsg = e.Message;
            }

            return dt;
        }

        /// <summary>
        /// 插入一条明细记录
        /// </summary>
        /// <param name="OprObject"></param>
        /// <returns></returns>
        public int Insert(clsCheckType model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sql.Length = 0;
                sql.Append("INSERT INTO TCHECKTYPE(NAME,ISREADONLY,ISLOCK,REMARK) VALUES(");
                sql.AppendFormat("'{0}',", model.Name);
                sql.AppendFormat("{0},", model.IsReadOnly);
                sql.AppendFormat("{0},", model.IsLock);
                sql.AppendFormat("'{0}'", model.Remark);
                sql.Append(")");
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
        /// 判断是否存在相同的名称
        /// </summary>
        /// <param name="sName"></param>
        /// <returns></returns>
        public static bool ExistSameValue(string sName)
        {
            string sErrMsg = string.Empty;

            try
            {
                string sql = string.Format("SELECT NAME FROM TCHECKTYPE WHERE NAME='{0}'", sName);
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
                return true;
            }
        }
    }
}
