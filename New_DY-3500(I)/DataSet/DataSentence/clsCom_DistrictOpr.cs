using System;
using System.Data;

namespace DataSetModel
{
	/// <summary>
	/// clsCom_DistrictOpr 的摘要说明。
	/// </summary>
	public class clsCom_DistrictOpr
	{
		private string Conncentstr;
		public clsCom_DistrictOpr(string Connstr)
		{
			Conncentstr=Connstr;
		}
		
		/// <summary>
		/// 根据查询串条件查询记录
		/// </summary>
		/// <param name="whereSql">查询条件串,不含Where</param>
		/// <param name="orderBySql">排序串,不含Order</param>
		/// <returns></returns>
		public DataTable GetAsDataTable(string whereSql, string orderBySql)
		{
			string sErrMsg=string.Empty; 
			DataTable dt=null;

            try
            {
                string selectSql = "select SysCode,StdCode,Name,ShortCut,DistrictIndex,CheckLevel,IsLocal,IsLock,IsReadOnly,Remark from tCom_District";

                if (!whereSql.Equals(string.Empty))
                {
                    selectSql += " where " + whereSql;
                }
                if (!orderBySql.Equals(string.Empty))
                {
                    selectSql += " order by " + orderBySql;
                }
                string[] cmd = new string[1] { selectSql };
                string[] names = new string[1] { "Com_District" };
                dt = DataBase.GetDataSet(Conncentstr, cmd, names, out sErrMsg).Tables["Com_District"];
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsCom_District",e);
                sErrMsg = e.Message;
            }

			return dt;
		}

        /// <summary>
        /// 判断是否存在,去掉第二参数，连接字符串。
        /// </summary>
        /// <param name="swhere"></param>
        /// <returns></returns>
        public static bool DistrictIsExist(string swhere)//,string Conncentstr
        {
            string sErrMsg = string.Empty;
            if (swhere.Equals(string.Empty))
            {
                return false;
            }

            try
            {
                string sql = "SELECT SYSCODE FROM TCOM_DISTRICT WHERE " + swhere;
                Object o = DataBase.GetOneValue(sql, out sErrMsg);//Conncentstr,
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
