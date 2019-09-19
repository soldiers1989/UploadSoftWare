using System;
using System.Data;

namespace DataSetModel
{
	/// <summary>
	/// clsCom_DistrictOpr ��ժҪ˵����
	/// </summary>
	public class clsCom_DistrictOpr
	{
		private string Conncentstr;
		public clsCom_DistrictOpr(string Connstr)
		{
			Conncentstr=Connstr;
		}
		
		/// <summary>
		/// ���ݲ�ѯ��������ѯ��¼
		/// </summary>
		/// <param name="whereSql">��ѯ������,����Where</param>
		/// <param name="orderBySql">����,����Order</param>
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
                //Log.WriteLog("��ѯclsCom_District",e);
                sErrMsg = e.Message;
            }

			return dt;
		}

        /// <summary>
        /// �ж��Ƿ����,ȥ���ڶ������������ַ�����
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
