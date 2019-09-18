using System;
using System.Data;
using System.Text;

namespace DYSeriesDataSet
{
    /// <summary>
    /// ��ⵥλ���ݲ�����
    /// ԭ���ߣ�����
    /// �ܶ��������룬��Ҫ�Ľ�
    /// ��ע��ԭ����дû���κ�ע��
    /// </summary>
    public class clsCheckComTypeOpr
    {
        public clsCheckComTypeOpr()
        {
        }
        private StringBuilder sb = new StringBuilder();
        /// <summary>
        /// �����޸ı���
        /// </summary>
        /// <param name="model">����clsCheckComType��һ��ʵ������</param>
        /// <returns></returns>
        public int UpdatePart(clsCheckComType model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length=0;
                sb.Append("UPDATE TCHECKCOMTYPE SET");
                sb.AppendFormat(" TypeName='{0}'", model.TypeName);
                sb.AppendFormat(",NameCall='{0}'", model.NameCall);
                sb.AppendFormat(",AreaCall='{0}'", model.AreaCall);
                sb.AppendFormat(",VerType='{0}'", model.VerType);
                sb.AppendFormat(",IsReadOnly={0}",model.IsReadOnly);
                sb.AppendFormat(",IsLock={0}",model.IsLock);
                sb.AppendFormat(",ComKind='{0}'",model.ComKind);
                sb.AppendFormat(",AreaTitle='{0}'",model.AreaTitle);
                sb.AppendFormat(",NameTitle='{0}'",model.NameTitle);
                sb.AppendFormat(",DomainTitle='{0}'",model.DomainTitle);
                sb.AppendFormat(",SampleTitle='{0}'", model.SampleTitle);
                sb.Append(" WHERE ID=");
                sb.Append(model.ID);

              
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

        /// <summary>
        /// �����޸ı���
        /// </summary>
        /// <param name="model">����clsCheckComType��һ��ʵ������</param>
        /// <returns></returns>
        public int UpdatePartTag(clsCheckComType model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length = 0;
                sb.Append("UPDATE TCHECKCOMTYPE SET");
             
                sb.AppendFormat(" NameCall='{0}'", model.NameCall);
                sb.AppendFormat(",AreaCall='{0}'", model.AreaCall);
                sb.AppendFormat(",AreaTitle='{0}'", model.AreaTitle);
                sb.AppendFormat(",NameTitle='{0}'", model.NameTitle);
                sb.AppendFormat(",DomainTitle='{0}'", model.DomainTitle);
                sb.AppendFormat(",SampleTitle='{0}'", model.SampleTitle);
                sb.Append(" WHERE ID=");
                sb.Append(model.ID);


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

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="strWhere">����</param>
        /// <param name="errMsg">���������Ϣ</param>
        /// <returns></returns>
        public int Delete(string strWhere, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length=0;
                sb.Append("DELETE FROM tcheckcomtype ");
                // string deleteSql = "delete from tcheckcomtype";

                if (strWhere != string.Empty)
                {
                    sb.Append(" WHERE ");
                    sb.Append(strWhere);
                    //deleteSql += " where " + whereSql;
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

        /// <summary>
        /// �����������ɾ����¼
        /// </summary>
        /// <param name="%pkname%">�������</param>
        /// <returns></returns>
        public int DeleteByPrimaryKey(string mainkey, out string sErrMsg)
        {
            sErrMsg = string.Empty;
            int rtn = 0;

            try
            {
                string deleteSql = string.Format("DELETE FROM tcheckcomtype WHERE ID={0}", mainkey);
                DataBase.ExecuteCommand(deleteSql, out sErrMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("ͨ������ɾ��clsCheckType",e);
                sErrMsg = e.Message; ;
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
            DataTable dtbl = null;
            sb.Length=0;
            try
            {
                string selectSql = string.Empty;
                if (type == 0)
                {
                    sb.Append("SELECT A.ID,A.TypeName,A.NameCall,A.AreaCall,A.VerType,A.IsLock,A.IsReadOnly,A.ComKind,B.Name As ComKindName,A.AreaTitle,A.NameTitle,A.DomainTitle,A.SampleTitle from tcheckcomtype As A Left Join tCompanyKind As B On A.ComKind=B.SysCode");
                }
                else if (type == 1)
                {
                    sb.Append("SELECT TypeName from tcheckcomtype");
                }
                else if (type == 2)
                {
                    sb.Append("SELECT TypeName,NameCall,AreaCall,AreaTitle,NameTitle,DomainTitle,SampleTitle FROM tcheckcomtype ");
                }
                else if (type == 3)
                {
                    sb.Append("SELECT TOP 1 * FROM tcheckcomtype ");
                }
                if (!whereSql.Equals(string.Empty))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                    //selectSql += " where " + whereSql;
                }
                if (!orderBySql.Equals(string.Empty))
                {
                    sb.Append(" ORDER BY ");
                    sb.Append(orderBySql);
                    //selectSql += " order by " + orderBySql;
                }
                string[] cmd = new string[1] { sb.ToString() };
                //sCmd[0] = selectSql;
                string[] names = new string[1] { "CheckComType" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["CheckComType"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return dtbl;
        }

        /// <summary>
        /// ����һ����ϸ��¼
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(clsCheckComType model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length=0;
                sb.Append("INSERT INTO tcheckcomtype(TypeName,NameCall,AreaCall,VerType,IsReadOnly,IsLock,ComKind,AreaTitle,NameTitle,DomainTitle,SampleTitle)VALUES('");
                sb.Append(model.TypeName);
                sb.Append("','");
                sb.Append(model.NameCall);
                sb.Append("','");
                sb.Append(model.AreaCall);
                sb.Append("','");
                sb.Append(model.VerType);
                sb.Append("',");
                sb.Append(model.IsReadOnly);
                sb.Append(",");
                sb.Append(model.IsLock);
                sb.Append(",'");
                sb.Append(model.ComKind);
                sb.Append("','");
                sb.Append(model.AreaTitle);
                sb.Append("','");
                sb.Append(model.NameTitle);
                sb.Append("','");
                sb.Append(model.DomainTitle);
                sb.Append("','");
                sb.Append(model.SampleTitle);
                sb.Append("')");
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
        /// �ж��Ƿ����
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool TypeNameIsExist(string name)
        {
            string errMsg = string.Empty;
            if (name.Equals(string.Empty))
            {
                return false;
            }

            try
            {
                string sql = string.Format("SELECT TYPENAME FROM tCheckComType WHERE TypeName='{0}'", name);
                Object o = DataBase.GetOneValue(sql, out errMsg);
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
                errMsg = e.Message;
                return false;
            }
        }
        /// <summary>
        /// �ж��Ƿ��ܱ�ɾ��
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool TypeIsNoDel(string name)
        {
            string errMsg = string.Empty;
            if (name.Equals(string.Empty))
            {
                return false;
            }

            try
            {
                string sql = string.Format("SELECT SYSCODE FROM TUSERUNIT WHERE SHORTCUT='{0}'", name);
                Object o = DataBase.GetOneValue(sql, out errMsg);
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
                errMsg = e.Message;
                return false;
            }
        }

        public static string ValueFromName(string file, string name)
        {
            string errMsg = string.Empty;
            if (name.Equals(string.Empty) || file.Equals(string.Empty))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT {0} FROM TCHECKCOMTYPE WHERE TYPENAME='{1}'", file, name);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                if (o == null)
                {
                    return string.Empty;
                }
                else
                {
                    return o.ToString();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }
    }
}
