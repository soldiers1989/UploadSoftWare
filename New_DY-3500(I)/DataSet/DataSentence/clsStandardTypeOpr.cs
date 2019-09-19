using System;
using System.Data;
using System.Text;

namespace DataSetModel
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
        private StringBuilder sb = new StringBuilder();

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
