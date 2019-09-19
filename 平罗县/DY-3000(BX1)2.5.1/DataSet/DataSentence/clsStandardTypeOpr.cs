using System;
using System.Data;
using System.Text;

namespace DYSeriesDataSet
{
    /// <summary>
    /// clsStandardTypeOpr ��ժҪ˵����
    /// </summary>
    public class clsStandardTypeOpr
    {
        public clsStandardTypeOpr()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        private StringBuilder sql = new StringBuilder();

        /// <summary>
        /// ����һ����ϸ��¼
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(clsStandardType model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sql.Length = 0;
                sql.Append("INSERT INTO TSTANDARDTYPE(STDNAME,ISREADONLY,ISLOCK,REMARK)");
                sql.Append(" VALUES(");
                sql.AppendFormat("'{0}',", model.StdName);
                sql.AppendFormat("{0},", model.IsReadOnly);
                sql.AppendFormat("{0},", model.IsLock);
                sql.AppendFormat("'{0}'", model.Remark);
                sql.Append(")");
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("���clsStandardType",e);
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
        public DataTable GetAsDataTable(string whereSql, string orderBySql, int type)
        {
            string errMsg = string.Empty;
            DataTable dt = null;

            try
            {
                sql.Length = 0;
                if (type == 0)
                {
                    sql.Append("SELECT STDNAME,ISLOCK,ISREADONLY,REMARK  FROM TSTANDARDTYPE");
                }
                else if (type == 1)
                {
                    sql.Append("select StdName from tStandardType");
                }

                if (!whereSql.Equals(""))
                {
                    sql.Append(" where ");
                    sql.Append(whereSql);
                }
                if (!orderBySql.Equals(""))
                {
                    sql.Append(" order by ");
                    sql.Append(orderBySql);
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "StandardType" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["StandardType"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("��ѯclsStandardType",e);
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
                sql.Length = 0;
                sql.Append("DELETE FROM TSTANDARDTYPE");

                if (!whereSql.Equals(""))
                {
                    sql.Append(" where ");
                    sql.Append(whereSql);
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
    }
}
