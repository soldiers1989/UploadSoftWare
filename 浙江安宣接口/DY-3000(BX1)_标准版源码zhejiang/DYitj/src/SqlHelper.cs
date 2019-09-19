using System;
using System.Data;
using System.Text;
using DYSeriesDataSet;

namespace AIO.src
{
    /// <summary>
    /// 数据库执行操作帮助类
    /// Create wenj
    /// Time 2017年6月8日
    /// </summary>
    public static class SqlHelper
    {
        private static StringBuilder sql = new StringBuilder();

        /// <summary>
        /// 根据表名+执行条件 灵活获取数据
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="where">执行条件</param>
        /// <param name="errMsg">返回异常信息</param>
        /// <returns>返回值</returns>
        public static DataTable GetDataTable(string table, string where, out string errMsg, string sqlContent = "")
        {
            errMsg = string.Empty;
            DataTable dtbl = null;
            sql.Length = 0;
            try
            {
                if (sqlContent.Length == 0)
                {
                    sql.Append(string.Format("SELECT * FROM {0}", table));
                    if (!where.Equals(string.Empty))
                    {
                        sql.AppendFormat(" Where {0}", where);
                    }
                }
                else
                {
                    sql.Append(sqlContent);
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "dtbl" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["dtbl"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return dtbl;
        }

        /// <summary>
        /// 根据表名+内容+执行条件 灵活update
        /// </summary>
        /// <param name="table">要更新的表名</param>
        /// <param name="content">更新的内容</param>
        /// <param name="where">执行条件</param>
        /// <param name="errMsg">异常信息</param>
        /// <returns>返回结果 1为成功</returns>
        public static int Update(string table, string content, string where, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.AppendFormat("Update {0} Set {1} Where {2}", table, content, where);
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
        /// 分页查询
        /// </summary>
        /// <param name="tbName"><表名0/param>
        /// <param name="whereSql">sql条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="PageIndex">当前页</param>
        /// <returns>返回数据集</returns>
        public static DataTable GetDtblByPage(string tbName, string whereSql, string orderBy, int PageSize, int PageIndex)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sql.Length = 0;
            try
            {
                if (PageIndex == 1)
                {
                    sql.AppendFormat("Select Top {0} * ", PageSize);
                    sql.AppendFormat("From {0} ", tbName);
                    if (whereSql.Length > 0)
                    {
                        sql.AppendFormat("Where {0} ", whereSql);
                    }
                }
                else
                {
                    //原分页方法效率太低
                    //sql.AppendFormat("Select Top {0} * ", PageSize);
                    //sql.AppendFormat("From {0} ", tbName);
                    //sql.Append(" Where ID Not In(Select top ");
                    //sql.Append(((PageIndex - 1) * PageSize).ToString());
                    //sql.AppendFormat(" ID From {0}) ", tbName);

                    sql.AppendFormat("Select Top {0} * ", PageSize);
                    sql.AppendFormat("From {0} ", tbName);
                    sql.AppendFormat(" WHERE (ID >= (SELECT MAX(id) FROM (SELECT TOP {0} id ", (PageIndex - 1) * PageSize);
                    sql.Append("FROM FoodStandard) AS T)) ");

                    if (whereSql.Length > 0)
                    {
                        sql.AppendFormat("And {0} ", whereSql);
                    }
                }

                if (orderBy.Length > 0)
                {
                    sql.AppendFormat("Order By {0}", orderBy);
                }

                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "dtbl" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["dtbl"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

    }
}