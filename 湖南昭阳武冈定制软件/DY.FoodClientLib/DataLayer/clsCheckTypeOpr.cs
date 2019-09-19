using System;
using System.Data;
using System.Text;

namespace DY.FoodClientLib
{
	/// <summary>
	/// ������
	/// </summary>
	public class clsCheckTypeOpr
	{
		public clsCheckTypeOpr()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        private StringBuilder sb = new StringBuilder();
		/// <summary>
		/// �����޸ı���
		/// </summary>
		/// <param name="model">����clsCheckType��һ��ʵ������</param>
		/// <returns></returns>
        public int UpdatePart(clsCheckType model, string sName, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length = 0;
                sb.Append("UPDATE TCHECKTYPE SET ");
                sb.AppendFormat("Name='{0}',", model.Name);
                sb.AppendFormat("IsReadOnly={0},", model.IsReadOnly);
                sb.AppendFormat("IsLock={0},", model.IsLock);
                sb.AppendFormat("Remark='{0}'", model.Remark);
                sb.AppendFormat(" WHERE Name='{0}'", sName);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;

                //������ر�
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
                //Log.WriteLog("����clsCheckType",e);
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// �Ƿ����ɾ��
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
                sb.Append("DELETE FROM TCHECKTYPE");

                if (!whereSql.Equals(""))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                }
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return rtn;
        }
		
		/// <summary>
		/// �����������ɾ����¼
		/// </summary>
		/// <param name="%pkname%">�������</param>
		/// <returns></returns>
        public int DeleteByPrimaryKey(string mainkey, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            sb.Length = 0;
            try
            {
                sb.Append("DELETE FROM TCHECKTYPE");
                sb.Append(" WHERE Name='");
                sb.Append(mainkey);
                sb.Append("'");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("ͨ������ɾ��clsCheckType",e);
                errMsg = e.Message; ;
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
                    sb.Append("SELECT NAME,ISLOCK,ISREADONLY,REMARK FROM TCHECKTYPE");
                }
                else if (type == 1)
                {
                    sb.Append("SELECT NAME FROM tchecktype");
                }

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
                string[] names = new string[1] { "CheckType" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["CheckType"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("��ѯclsCheckType",e);
                errMsg = e.Message;
            }

            return dt;
        }

		/// <summary>
		/// ����һ����ϸ��¼
		/// </summary>
		/// <param name="OprObject"></param>
		/// <returns></returns>
        public int Insert(clsCheckType model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length = 0;
                sb.Append("INSERT INTO TCHECKTYPE(NAME,ISREADONLY,ISLOCK,REMARK) VALUES(");
                sb.AppendFormat("'{0}',", model.Name);
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
                //Log.WriteLog("���clsCheckType",e);
                errMsg = e.Message;
            }

            return rtn;
        }
        /// <summary>
        /// �ж��Ƿ������ͬ������
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
