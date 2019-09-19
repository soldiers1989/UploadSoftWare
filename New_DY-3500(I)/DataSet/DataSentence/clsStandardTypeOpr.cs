using System;
using System.Data;
using System.Text;

namespace DataSetModel
{
	/// <summary>
	/// clsStandardTypeOpr 的摘要说明。
	/// </summary>
    public class clsStandardTypeOpr
    {
        public clsStandardTypeOpr()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 插入一条明细记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(clsStandardType model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length = 0;
                sb.Append("INSERT INTO TSTANDARDTYPE(STDNAME,ISREADONLY,ISLOCK,REMARK)");
                sb.Append(" VALUES(");
                sb.AppendFormat("'{0}',", model.StdName);
                sb.AppendFormat("{0},", model.IsReadOnly);
                sb.AppendFormat("{0},", model.IsLock);
                sb.AppendFormat("'{0}'", model.Remark);
                sb.Append(")");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("添加clsStandardType",e);
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
            DataTable dt = null;

            try
            {
                sb.Length = 0;
                if (type == 0)
                {
                    sb.Append("SELECT STDNAME,ISLOCK,ISREADONLY,REMARK  FROM TSTANDARDTYPE");
                }
                else if (type == 1)
                {
                    sb.Append("select StdName from tStandardType");
                }

                if (!whereSql.Equals(string.Empty))
                {
                    sb.Append(" where ");
                    sb.Append(whereSql);
                }
                if (!orderBySql.Equals(string.Empty))
                {
                    sb.Append(" order by ");
                    sb.Append(orderBySql);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "StandardType" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["StandardType"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsStandardType",e);
                errMsg = e.Message;
            }

            return dt;
        }

        /// <summary>
        /// 删除操作
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
                sb.Length = 0;
                sb.Append("DELETE FROM TSTANDARDTYPE");

                if (!whereSql.Equals(string.Empty))
                {
                    sb.Append(" where ");
                    sb.Append(whereSql);
                }
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
    }
}
