using System;
using System.Data;
using System.Text;
using DYSeriesDataSet;

namespace AIO.src
{
    public static class opr
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
        /// 根据表名+执行条件 灵活获取数据
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="where">执行条件</param>
        /// <param name="errMsg">返回异常信息</param>
        /// <returns>返回值</returns>
        public static DataTable GetAsDataTable(string table, string where, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtbl = null;
            sql.Length = 0;
            try
            {
                sql.Append(string.Format("SELECT * FROM {0}", table));
                if (!where.Equals(string.Empty))
                {
                    sql.AppendFormat(" Where {0}", where);
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

        public static int OtherOpr(string sql, out string errMsg)
        {
            errMsg = string.Empty;
            try
            {
                DataBase.ExecuteCommand(sql, out errMsg);
                return 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 0;
            }
        }

    }
}