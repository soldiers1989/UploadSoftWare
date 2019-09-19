using System;
using System.Data;
using System.Text;

namespace DataSetModel
{
	/// <summary>
	/// 检测标准
	/// </summary>
	public class clsCheckLevelOpr
	{
		public clsCheckLevelOpr()
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
        public int Insert(clsCheckLevel model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length = 0;
                sb.Append("INSERT INTO TCHECKLEVEL(CHECKLEVEL,ISREADONLY,ISLOCK,REMARK)VALUES(");
                sb.AppendFormat("'{0}',", model.CheckLevel);
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
                //Log.WriteLog("添加clsCheckLevel",e);
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
        public DataTable GetAsDataTable(string whereSql, string orderBySql)
        {
            string errMsg = string.Empty;
            DataTable dt = null;

            try
            {
                sb.Length = 0;
                sb.Append("SELECT CHECKLEVEL FROM TCHECKLEVEL");
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
                string[] names = new string[1] { "CheckLevel" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["CheckLevel"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsCheckLevel",e);
                errMsg = e.Message;
            }

            return dt;
        }
        /// <summary>
        /// 删除
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
                sb.Append("DELETE FROM tCheckLevel");

                if (!whereSql.Equals(string.Empty))
                {
                    sb.Append(" WHERE ");
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
