using System;
using System.Data;
using System.Text;

namespace DYSeriesDataSet
{
	/// <summary>
	/// ��λ����
	/// </summary>
	public class clsCompanyPropertyOpr
	{
		public clsCompanyPropertyOpr()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
        private StringBuilder sb = new StringBuilder();

		/// <summary>
		/// ����һ����ϸ��¼
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
        public int Insert(clsCompanyProperty model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length = 0;
                sb.Append("insert into tCompanyProperty(CompanyProperty,IsReadOnly,IsLock,Remark)");
                sb.Append(" values(");
                sb.AppendFormat("'{0}',", model.CompanyProperty);
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
                //Log.WriteLog("���clsCompanyProperty",e);
                errMsg = e.Message;
            }

            return rtn;
        }

		/// <summary>
		/// ���ݲ�ѯ��������ѯ��¼
		/// </summary>
		/// <param name="whereSql">��ѯ������,����Where</param>
		/// <param name="orderBySql">����,����Order</param>
		/// <returns></returns>
		public DataTable GetAsDataTable(string whereSql, string orderBySql)
		{
			string errMsg=string.Empty; 
			DataTable dt=null;

            try
            {
                sb.Length = 0;
                sb.Append("SELECT COMPANYPROPERTY FROM TCOMPANYPROPERTY");
                if (!whereSql.Equals(""))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                }
                if (!orderBySql.Equals(""))
                {
                    sb.Append(" ORDER BY ");
                    sb.Append(orderBySql);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "CompanyProperty" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["CompanyProperty"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("��ѯclsCompanyProperty",e);
                errMsg = e.Message;
            }

			return dt;
		}
        /// <summary>
        /// ɾ������
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
                sb.Append("DELETE FROM TCOMPANYPROPERTY");

                if (!whereSql.Equals(""))
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
